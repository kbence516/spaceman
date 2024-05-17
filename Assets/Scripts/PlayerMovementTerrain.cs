using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTerrain : MonoBehaviour
{
    public float speed = 1f;
    public float jumpForce = 1f;
    public static bool facingRight = true;
    public bool onGround = false;

    private Rigidbody2D rigidBody;
    private Animator animator;
    private PlayerDamage playerDamage;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerDamage = GetComponent<PlayerDamage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDamage.hp > 0) {
            float movementX = Input.GetAxis("Horizontal");
            Vector2 velocity = rigidBody.velocity;
            velocity.x = speed * movementX;
            rigidBody.velocity = velocity;
            animator.SetFloat("absMovementX", Mathf.Abs(movementX));

            if (facingRight && movementX < 0) {
                Flip();
            } else if (!facingRight && movementX > 0) {
                Flip();
            }

            if (Input.GetButtonDown("Jump")) {
                if (onGround) {
                    rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
            }
        }

        onGround = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
    }

    private void Flip() {
        facingRight = !facingRight;

        Vector3 playerScale = this.transform.localScale;
        playerScale.x *= -1;
        this.transform.localScale = playerScale;
    }
}
