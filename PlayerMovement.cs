using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 2f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    public Transform spawnPoint;

    [Header("Jump")]
    public float jumpForce = 10f;
    private bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") < 0) {
            sr.flipX = true;
            spawnPoint.localPosition = new Vector3(-0.3f, 0,0);
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            spawnPoint.localPosition = new Vector3(0.3f, 0,0);
            sr.flipX = false;
        }
            
        rb.velocity = new Vector2(walkSpeed * Input.GetAxis("Horizontal") , rb.velocity.y);
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("yVelocity", rb.velocity.y);

        if(Input.GetButtonDown("Fire1"))
            animator.SetTrigger("shot");

        if(Input.GetKeyDown("space") && isGrounded) {
            Jump();
        }

        if(Mathf.Abs(rb.velocity.x) >= walkSpeed)
            animator.SetBool("isRunning", true);
        else if(Mathf.Abs(rb.velocity.x) < walkSpeed)
            animator.SetBool("isRunning", false);
    }

    void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }
}
