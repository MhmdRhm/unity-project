using UnityEngine;
using UnityEngine.Timeline;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpStrength = 4f;
    public Animator cameraAnimator;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask attackableLayer;
    public GameManager manager;
    public float attackCooldown = 2f;
    public GameObject loadingUI;
    private Vector2 direction;
    private new Rigidbody2D rigidbody;
    private Collider2D playerCollider;
    private Animator animator;
    private Collider2D[] results;
    private bool isGrounded;
    private bool isClimbing;
    private bool canMove = true;
    private bool hasHammer;
    private float lastAttackTime = -Mathf.Infinity;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void CheckCollision() {
        isGrounded = false;
        isClimbing = false;

        Vector2 size = playerCollider.bounds.size;
        size.x /= 2f;
        size.y += 0.1f;

        results = Physics2D.OverlapBoxAll(transform.position, size, 0);
        for (int i = 0; i < results.Length; i++) {
            GameObject hit = results[i].gameObject;
            if (hit.layer == LayerMask.NameToLayer("Ground")) {
                isGrounded = hit.transform.position.y < (transform.position.y - 0.1f);
                Physics2D.IgnoreCollision(playerCollider, results[i], !isGrounded);
            } else if (hit.layer == LayerMask.NameToLayer("Ladder")) {
                isClimbing = true;
            }
        }
    }

    private void Attack()
    {
        lastAttackTime = Time.time;
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackableLayer);
        if (hit != null)
        {
            hit.gameObject.SetActive(false);
            int count = manager.DecrementDestoyredBarrels();
            if (count == 0)
            {
                hasHammer = false;
                manager.DeactivateObjectiveBarrelText();
                manager.ResetLadderColors();
            }
        }
    }

    private bool CanAttack()
    {
        bool res = Time.time >= lastAttackTime + attackCooldown;
        if (!res)
            loadingUI.SetActive(true);
        else
            loadingUI.SetActive(false);
        return res;
    }

    private void Update()
    {
        CheckCollision();
        if (!hasHammer)
        {
            if (isClimbing)
            {
                direction.y = Input.GetAxis("Vertical") * moveSpeed;
                animator.SetBool("isClimbing", true);
            }
            else
            {
                animator.SetBool("isClimbing", false);
                if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
                {
                    direction.y = jumpStrength;
                }
                else
                {
                    direction += Physics2D.gravity * Time.deltaTime;
                }
            }
        }
        else
        {
            direction += Physics2D.gravity * Time.deltaTime;
            if (CanAttack() && Input.GetMouseButtonDown(0))
            {
                AudioManager.Instance.PlayAttackClip();
                animator.SetTrigger("attack");
                DeactivateMovement();
                Invoke(nameof(ActivateMovement), 0.2f);
                Attack();
            }
        }

        if (canMove)
            direction.x = Input.GetAxis("Horizontal") * moveSpeed;

        animator.SetFloat("xVelocity", Mathf.Abs(direction.x));
        cameraAnimator.SetFloat("yPosition", transform.position.y);

        if (isGrounded)
            direction.y = Mathf.Max(direction.y, -1f);

        if (direction.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    private void DeactivateMovement()
    {
        canMove = false;
        direction.x = 0;
    }

    private void ActivateMovement()
    {
        canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Objective"))
        {
            enabled = false;
            AudioManager.Instance.PlayGameCompleteClip();
            manager.Zoom();
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.Instance.PlayHitClip();
            manager.Hit();
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("HammerPowerup"))
        {
            AudioManager.Instance.PlayPowerupClip();
            manager.ActivateObjectiveBarrelText();
            manager.SetLaddersToGray();
            hasHammer = true;
            Destroy(other.gameObject);
        }
    }
    private void FixedUpdate()
    {
        rigidbody.velocity = direction;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
