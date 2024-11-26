using UnityEngine;

public class R_KeepDistance : State
{
    [SerializeField] private float minDistance;
    [SerializeField] private float attackRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.forward = (player.transform.position - animator.transform.position).normalized;

        float distance = Vector3.Distance(player.position, animator.transform.position);
        Debug.Log(distance);
        if (distance >= minDistance && distance <= attackRange)
        {
            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsKeepDistance", false);
            agent.isStopped = true;
        }

        Vector3 direction = (animator.transform.position - player.transform.position).normalized;

        Vector3 destinationPoint = player.transform.position + direction * attackRange;
        agent.destination = destinationPoint;
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
