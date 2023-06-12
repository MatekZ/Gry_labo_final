using System.Collections;
using System.Collections.Generic;
//using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.Rendering;

public class FallingSpikes : MonoBehaviour
{
    private Rigidbody2D rb;
    private RaycastHit2D playerHit;
    [SerializeField]
    private LayerMask playerLayer;
    public float detection;
    public int damage = 10;
    public Vector2 knockback = new Vector2(0, 0);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CollisionDetection();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Damageable damageable = collision.GetComponent<Damageable>();


        if (damageable != null && collision.CompareTag("Player"))
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.PlayerHit(damage, deliveredKnockback);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void CollisionDetection()
    {
        playerHit = Physics2D.Raycast(transform.position, Vector2.down, detection, playerLayer);

        Debug.DrawRay(transform.position, Vector2.down * detection);
        if (playerHit)
        {
            rb.gravityScale = 2f;
        }
    }
}
