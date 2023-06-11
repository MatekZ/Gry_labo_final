using Kryz.CharacterStats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    public StatPanel statPanel;
    public int attack;
    /* private void Awake()
     {
         attackCollider = GetComponent<Collider2D>();
     }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("player hit");
                Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                bool gotHit = damageable.PlayerHit(attackDamage, deliveredKnockback);
            }
            else if (collision.CompareTag("Enemy"))
            {
                Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                bool gotHit = damageable.EnemyHit(attackDamage, deliveredKnockback);
            }
            else if (collision.CompareTag("Boss"))
            {
                Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
                bool gotHit = damageable.BossHit(attackDamage, deliveredKnockback);
            }



        }
    }


    public void ChangeStats()
    {
        CharacterStat[] stats = statPanel.GetStats();

        attackDamage = attackDamage - attack + ((int)stats[0].Value);
        attack = ((int)stats[0].Value);
    }
}
