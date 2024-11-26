using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ChangeWeaponState : State
{
    private BossZombie bossZombie;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        bossZombie = animator.GetComponent<BossZombie>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.forward = (player.transform.position - animator.transform.position).normalized;

        float distance = Vector3.Distance(player.position, animator.transform.position);
        //Debug.Log(distance);

        animator.SetBool("IsMeleeOrRange", bossZombie.GetIsMeleeOrRange());

        if (bossZombie.GetIsMeleeOrRange() == true)         // True: Melee
        {
            if (distance > bossZombie.GetHitAttackRange())
            {
                animator.SetBool("IsAttackingMelee", false);
                animator.SetBool("IsChasing", true);

                agent.isStopped = false;
                return;
            }
        }

        else                                                // False: Range 
        {
            if (distance > bossZombie.GetShootAttackRange())
            {
                animator.SetBool("IsAttackingRange", false);
                animator.SetBool("IsChasing", true);

                agent.isStopped = false;
                return;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

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
