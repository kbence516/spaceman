using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour {
    public bool inSpace = false;
    public List<Enemy> enemies;
    public float lineOfSight = 3f;
    public TMPro.TextMeshProUGUI enemiesCountText;
    public Image enemiesTaskImage;
    public Sprite checkedTask;

    private Transform player;

    void Awake() {
        enemies = new List<Enemy>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Register(Enemy enemy) {
        this.enemies.Add(enemy);
    }

    public void Unregister(Enemy enemy) {
        this.enemies.Remove(enemy);
    }

    public void Update() {
        if (!inSpace) {
            foreach (Enemy enemy in enemies) {
                LookAtPlayer(player, enemy.transform);
            }
        }
    }

    public void LookAtPlayer(Transform player, Transform enemy) {
        float distanceFromPlayer = Vector3.Distance(player.position, enemy.position);
        if (distanceFromPlayer <= lineOfSight) {
            if ((player.position.x < enemy.position.x && enemy.localScale.x > 0)
                || (player.position.x > enemy.position.x && enemy.localScale.x < 0)) {
                Vector3 enemyScale = enemy.localScale;
                enemyScale.x *= -1;
                enemy.localScale = enemyScale;
            }
        }
    }

    public void AddScore() {
        LoadNextLevel.points++;
        enemiesCountText.text = LoadNextLevel.points + " / " + LoadNextLevel.maxPoints;
        if (LoadNextLevel.points == LoadNextLevel.maxPoints) {
            enemiesTaskImage.sprite = checkedTask;
        }
    }
}
