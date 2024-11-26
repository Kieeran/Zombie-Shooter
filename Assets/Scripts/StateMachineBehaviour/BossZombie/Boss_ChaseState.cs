using UnityEngine;

public class Boss_ChaseState : State
{
    private BossZombie bossZombie;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        bossZombie = animator.GetComponent<BossZombie>();

        agent.speed = bossZombie.GetChaseSpeed();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.forward = (player.transform.position - animator.transform.position).normalized;

        float distance = Vector3.Distance(player.position, animator.transform.position);
        //Debug.Log(distance);
        if (distance > bossZombie.GetChaseRange())
        {
            animator.SetBool("IsChasing", false);
            animator.SetBool("IsIdling", true);
            agent.isStopped = true;
            return;
        }

        if (bossZombie.GetIsMeleeOrRange() == true)          // True: Melee  
        {
            if (distance <= bossZombie.GetHitAttackRange())
            {
                animator.SetBool("IsAttackingMelee", true);
                animator.SetBool("IsChasing", false);
                agent.isStopped = true;
                return;
            }
        }

        else                                                // False: Range 
        {
            if (distance <= bossZombie.GetShootAttackRange())
            {
                animator.SetBool("IsAttackingRange", true);
                animator.SetBool("IsChasing", false);
                agent.isStopped = true;
                return;
            }
        }

        agent.destination = player.position;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.destination = animator.transform.position;
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
