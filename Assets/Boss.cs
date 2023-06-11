using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public Transform player;

    public bool isFlipped = false;
    public Damageable damageable;
    public GameObject HPBar;
    public Animator animator;
    public GameObject enemyPrefab, projectilePrefab;
    public Transform[] enemySpawnPoints, projectileSpawnPoints;
    public Rigidbody2D rb;
    public float jumpImpulse, returnSpeed;
    private Vector2 initialPosition;
    public GameObject dialoguePanel;
    private bool phase2Activated = false;
    public Animator phase2Animator;
    public CameraShake phase2CamShake; 






    void Start()
    {
        initialPosition = transform.position;
    }


    private void Update()
    {
        rb.transform.position = new Vector2(rb.transform.position.x, 0.55f);
        DestroyHPBar();
        Phase2();
        
        /*if (phase2Start == true)
        {
            StartCoroutine(Phase2Start());
        }
        else
        {
            StopCoroutine(Phase2Start());
        }*/
    }


    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        //flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }



    public void DestroyHPBar()
    {
        if (damageable.Health <= 0)
        {
            Destroy(HPBar);
        }
    }

    public void Phase2()
    {
        if (damageable.Health <= 20 && !phase2Activated)
        {
            phase2Activated = true;
            animator.SetBool("phase2", true);
            animator.SetTrigger("phase2start");

        }
        
    }



    public void SummonEnemy()
    {
        int rnd = Random.Range(0, (enemySpawnPoints.Length));
        //. Debug.Log(rnd);
        Instantiate(enemyPrefab, enemySpawnPoints[rnd].position, Quaternion.identity);
    }

    public void SummonProjectile()
    {
        int rnd1 = Random.Range(3, 9);

        for (int i = 0; i <= rnd1; i++)
        {
            int rnd = Random.Range(0, (projectileSpawnPoints.Length));
            Debug.Log(rnd);
            Instantiate(projectilePrefab, projectileSpawnPoints[rnd].position, Quaternion.identity);
        }
    }

    public void BossTP()
    {
        transform.position = new Vector2(player.transform.position.x + 1f, 0f);
    }

   /* IEnumerator Phase2Start()
    {
        *//*transform.position = new Vector2(0f, 0f);
        dialoguePanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        phase2Start = false;*//*
        
    }*/
}