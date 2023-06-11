using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPoint : MonoBehaviour
{
    AudioSource healAudio;


    private void OnTriggerStay2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (collision.CompareTag("Player") && damageable && damageable.Health < damageable.MaxHealth)
        {

            bool wasHealed = damageable.Heal((damageable.MaxHealth - damageable.Health));

            if (wasHealed)
            {
                if (healAudio)
                {
                    AudioSource.PlayClipAtPoint(healAudio.clip, gameObject.transform.position, healAudio.volume);
                }
            }


        }
    }
}
