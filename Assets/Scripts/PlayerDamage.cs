using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour {
    public int maxHp = 5;
    public int hp;
    public Transform deathPoint;
    public Image healthBar;

    private float maxDamageCooldown = 1f;
    private float damageCooldown = 0;

    private Animator animator;
    private LoadNextLevel loadNextLevel;
    private RespawnManager respawnManager;

    private void Start() {
        hp = maxHp;
        animator = GetComponent<Animator>();
        loadNextLevel = GameObject.FindGameObjectWithTag("Finish")?.GetComponent<LoadNextLevel>();
        respawnManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>();
        healthBar = GameObject.Find("HPSlider").GetComponent<Image>();
    }

    void Update() {
        damageCooldown += Time.deltaTime;
        animator.SetInteger("hp", hp);
        if (deathPoint != null && transform.position.y < deathPoint.position.y) {
            StartCoroutine(Die());
        }

        if (hp <= 0) {
            StartCoroutine(Die(RespawnManager.inSpace ? 0.4f : 2f));
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Enemy" && damageCooldown >= maxDamageCooldown) {     // TODO: Rockets should have polygon colliders instead of box and edge colliders
            hp--;
            animator.SetTrigger("hasHurt");
            damageCooldown = 0;
            healthBar.fillAmount = (float)hp / maxHp;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Enemy" && damageCooldown >= maxDamageCooldown) {
            hp--;
            animator.SetTrigger("hasHurt");
            damageCooldown = 0;
            healthBar.fillAmount = (float)hp / maxHp;
        }
    }

    private IEnumerator Die(float animationLength = 0f) {
        yield return new WaitForSeconds(animationLength);
        //hp = maxHp;
        if (loadNextLevel != null) {
            LoadNextLevel.collectibles = 0;
            LoadNextLevel.points = 0;
        }
        gameObject.SetActive(false);
        SceneManager.LoadScene("DeathScreen", LoadSceneMode.Single);
        //GameObject.FindGameObjectWithTag("GameManager").GetComponent<RespawnManager>().Reset();
    }

}
