using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Transform target;
    private GameManager manager;
    public float speed = 1f;
    private new Rigidbody2D rigidbody;
    private Vector2 direction;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void Start() {
        direction = (Vector2)target.position - rigidbody.position;
        direction.Normalize();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponent<PlayerController>().enabled)
        {
            manager.Hit();
            AudioManager.Instance.PlayHitClip();
            Destroy(gameObject);
        }
    }
}
