using UnityEngine;
using UnityEngine.AI;

public class State : StateMachineBehaviour
{
    protected Transform player;
    protected NavMeshAgent agent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        player = PlayerManager.Instance.GetPlayerTransform();
        agent = animator.GetComponent<NavMeshAgent>();
    }
}
