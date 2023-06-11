using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phase2SetBehaviour : StateMachineBehaviour
{
    Rigidbody2D rb;
    Boss boss;
    public float phase2StartTime, phase2Timer, phase2ExeTime;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
        rb = animator.GetComponent<Rigidbody2D>();
        rb.transform.position = Vector3.zero;
        boss.dialoguePanel.SetActive(true);
        boss.phase2Animator.SetBool("phase2cutscene", true);
        boss.phase2CamShake.Shake();

        phase2StartTime = Time.time;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        phase2Timer = Time.time;

        if (phase2Timer - phase2StartTime > phase2ExeTime)
        {
            boss.dialoguePanel.SetActive(false);
            animator.ResetTrigger("phase2start");
            animator.SetBool("phase2end", true);
            boss.phase2Animator.SetBool("phase2cutscene", false);

        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
