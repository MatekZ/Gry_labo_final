using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class BossRunBehaviour : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float moveSpeed;
    public float attackRange, attackCooldown, specialAttackCooldown, tpCooldown;
    public float lastAttack = -10f;
    public float lastSpecialAttack = -100f;
    public float lastTP = -100f;
    Boss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange && (Time.time - lastAttack > attackCooldown))
        {
            int rnd = Random.Range(1, 4);
            //Debug.Log(rnd);

            switch (rnd)
            {
                case 1:
                    animator.SetTrigger("attack1");
                    lastAttack = Time.time;
                    break;
                case 2:
                    animator.SetTrigger("attack2");
                    lastAttack = Time.time;
                    break;
                /*case 3:
                    animator.SetTrigger("jumpAttack");
                    boss.BossJump();
                    lastAttack = Time.time;
                    boss.BossEndJump();
                    break;*/
            }
        }
        else if (Vector2.Distance(player.position, rb.position) > attackRange && (Time.time - lastTP > tpCooldown))
        {
            animator.SetTrigger("teleport");
            boss.BossTP();
            lastTP = Time.time;

        }
        else if (Vector2.Distance(player.position, rb.position) > attackRange && (Time.time - lastSpecialAttack > specialAttackCooldown) && animator.GetBool("phase2") == true)
        {
            int rnd = Random.Range(4, 6);
            //Debug.Log(rnd);
            switch (rnd)
            {
                case 4:
                    animator.SetTrigger("summonEnemy");
                    lastSpecialAttack = Time.time;
                    lastAttack = Time.time;

                    break;
                case 5:
                    animator.SetTrigger("spikeAttack");
                    lastSpecialAttack = Time.time;
                    lastAttack = Time.time;

                    break;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack1");
        animator.ResetTrigger("attack2");
        animator.ResetTrigger("teleport");
        animator.ResetTrigger("summonEnemy");
        animator.ResetTrigger("spikeAttack");

    }
}
