using UnityEngine;

public class Boss_IdleState : State
{
    private float timer;
    [SerializeField] private float delay;

    private BossZombie bossZombie;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timer = 0f;

        bossZombie = animator.GetComponent<BossZombie>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdling", false);
            agent.isStopped = false;
            return;
        }

        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance <= bossZombie.GetChaseRange())
        {
            animator.SetBool("IsChasing", true);
            animator.SetBool("IsIdling", false);
            agent.isStopped = false;
            return;
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
