using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationAI : MonoBehaviour
{
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private Animator animator;

    public void AddWayPoint(Transform wayPoint)
    {
        waypoints.Add(wayPoint);
    }

    private Transform player;
    private NavMeshAgent agent;
    private Transform randomWayPoint;
    private bool doneSetRandomWayPoint;

    void Start()
    {
        if (GetComponent<NavMeshAgent>().isOnNavMesh == false)
        {
            // GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = true;
        }

        agent = GetComponent<NavMeshAgent>();

        player = PlayerManager.Instance.GetPlayerTransform();
        GetRandomWayPoint();
        doneSetRandomWayPoint = false;
    }

    private void GetRandomWayPoint()
    {
        int index = Random.Range(0, waypoints.Count - 1);

        if (randomWayPoint == waypoints[index])
            GetRandomWayPoint();
        else
        {
            randomWayPoint = waypoints[index];
            return;
        }
    }

    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, player.position));

        if (animator.GetBool("IsDead")) return;

        if (animator.GetBool("IsWalking") == true)
        {
            //Debug.Log(randomWayPoint.gameObject.name + " : " + randomWayPoint.position);
            agent.destination = randomWayPoint.position;
            doneSetRandomWayPoint = false;
        }
        else if (animator.GetBool("IsWalking") == false)
        {
            if (animator.GetBool("IsIdling") == true)
            {
                if (doneSetRandomWayPoint == false)
                {

                    //agent.destination = transform.position;

                    GetRandomWayPoint();
                    doneSetRandomWayPoint = true;
                }
            }
        }
    }
}