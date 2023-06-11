using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage = 10;
    public Vector2 knockback = new Vector2(0, 0);
    private float hitCooldown = 0.5f;
    private float lastHit;



    private void OnTriggerEnter2D(Collider2D collision)
    {

        Damageable damageable = collision.GetComponent<Damageable>();


        if (damageable != null && collision.CompareTag("Player"))
        {
            if (Time.time - lastHit > hitCooldown)
            {
                Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                bool gotHit = damageable.PlayerHit(damage, deliveredKnockback);
                lastHit = Time.time;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null && collision.CompareTag("Player"))
        {
            if (Time.time - lastHit > hitCooldown)
            {
                Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                bool gotHit = damageable.PlayerHit(damage, deliveredKnockback);
                lastHit = Time.time;
            }
        }
    }

}


