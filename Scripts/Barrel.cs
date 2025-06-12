using UnityEngine;

public class Barrel : MonoBehaviour
{
    private new Rigidbody2D rigidbody;
    public float barrelSpeed = 3f;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if(transform.position.y < -10)
            gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        GameObject hit = other.gameObject;
        if (hit.layer == LayerMask.NameToLayer("Ground")) {
            rigidbody.AddForce(hit.transform.right * barrelSpeed, ForceMode2D.Impulse);
        }
    }
}
