using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 10;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.EnemyHit(damage, deliveredKnockback);
            Destroy(gameObject);

        }
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Damageable damageable = collision.GetComponent<Damageable>();


        if (damageable != null)
        {
            if (collision.CompareTag("Player"))
            {
                Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                bool gotHit = damageable.PlayerHit(damage, deliveredKnockback);
                Destroy(gameObject);
            }
            else if (collision.CompareTag("Enemy"))
            {

                Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                bool gotHit = damageable.EnemyHit(damage, deliveredKnockback);
                Destroy(gameObject);
            }
            else if (collision.CompareTag("Boss"))
            {
                Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                bool gotHit = damageable.BossHit(damage, deliveredKnockback);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);

        }
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    /*if (gotHit)
    {
        Destroy(gameObject);
    }

}
if (damageable != null)
{
    Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
    bool gotHit = damageable.Hit(damage, deliveredKnockback);

    if (gotHit)
    {
        Destroy(gameObject);
    }
}*/

}
