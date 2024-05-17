using UnityEngine;

public class BulletManager : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision) {
        Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();
        ParticleSystem robotSparkles = collision.gameObject.GetComponentInChildren<ParticleSystem>();
        this.gameObject.SetActive(false);
        if (collision.gameObject.tag == "Enemy") {
            enemyAnimator.SetTrigger("hasHurt");
            collision.gameObject.GetComponent<EnemyDamage>().hp--;
            robotSparkles.Emit(120);
        }
    }
}
