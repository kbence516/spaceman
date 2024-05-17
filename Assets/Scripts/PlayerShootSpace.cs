using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerShootSpace : MonoBehaviour {
    private Rigidbody2D rigidBody;
    private Rigidbody2D player;
    private Animator animator;

    public Rigidbody2D bulletProto = null;
    public float speed = 1.0f;
    public Transform firePoint;

    public int bulletPoolSize = 3;
    public List<Rigidbody2D> bulletPool;

    public float maxDistanceFromPlayer = 5.0f;

    public float timeSinceLastShot = 0f;
    public float shootingCooldown = 1f;

    public float knockbackForce = 1f;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bulletPool = new List<Rigidbody2D>();

        for (int i = 0; i < bulletPoolSize; i++) {
            Rigidbody2D bulletClone = Instantiate(bulletProto);
            bulletClone.gameObject.SetActive(false);

            bulletPool.Add(bulletClone);
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            if (timeSinceLastShot >= shootingCooldown) {
                Shoot();
            }
        }

        foreach (Rigidbody2D bullet in bulletPool) {
            if (bullet.gameObject.activeSelf && DistanceFromPlayer(bullet) > maxDistanceFromPlayer) {
                bullet.gameObject.SetActive(false);
            }
        }

        timeSinceLastShot += Time.deltaTime;
    }

    void Shoot() {
        Rigidbody2D bulletClone = GetBulletFromPool();
        bulletClone.transform.position = firePoint.position;
        bulletClone.gameObject.SetActive(true);
        bulletClone.velocity = player.transform.up * speed;

        animator.SetTrigger("hasShot");
        timeSinceLastShot = 0f;
    }

    private Rigidbody2D GetBulletFromPool() {

        foreach (Rigidbody2D bullet in bulletPool) {
            if (!bullet.gameObject.activeSelf) {
                return bullet;
            }
        }
        return null;
    }

    private float DistanceFromPlayer(Rigidbody2D bullet) {
        return Vector2.Distance(bullet.position, transform.position);
    }
}
