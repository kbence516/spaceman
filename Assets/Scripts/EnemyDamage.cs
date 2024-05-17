using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour {
    public int hp = 3;
    public int maxHp = 3;
    public Transform deathPoint;
    private Animator animator;
    private LoadNextLevel loadNextLevel;
    private EnemiesManager enemiesManager;

    private void Start() {
        deathPoint = GameObject.Find("DeathPoint")?.transform;
        animator = GetComponent<Animator>();
        loadNextLevel = GameObject.FindGameObjectWithTag("Finish")?.GetComponent<LoadNextLevel>();
        enemiesManager = GameObject.Find("Enemies").GetComponent<EnemiesManager>();
    }

    void Update() {
        animator.SetInteger("hp", hp);
        if (deathPoint != null && transform.position.y < deathPoint.position.y) {
            StartCoroutine(Die());
        }

        if (hp <= 0) {
            StartCoroutine(Die(RespawnManager.inSpace ? 0.2f : 0.75f));
        }
    }

    private IEnumerator Die(float animationLength = 0f) {
        yield return new WaitForSeconds(animationLength);
        gameObject.SetActive(false);
        enemiesManager.AddScore();
        //hp = maxHp;
    }
}
