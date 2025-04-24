using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float destroyDelay = 3f;
    
    private SpriteRenderer sr;
    public float bulletSpeed = 1000f;
    private Vector2  bulletDirection;
    void Start() {
        sr = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
        bulletDirection = sr.flipX ? Vector2.left : Vector2.right;
        Destroy(gameObject, destroyDelay);
    }
    void FixedUpdate()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = bulletDirection * bulletSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
