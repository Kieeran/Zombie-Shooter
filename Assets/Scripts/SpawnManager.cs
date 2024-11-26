using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;
    }

    [SerializeField] private List<Spawn> MainZone;
    [SerializeField] private List<Spawn> AZone;
    [SerializeField] private List<Spawn> BZone;
    [SerializeField] private List<Spawn> CZone;
    [SerializeField] private List<Spawn> BossZone;

    private void Start()
    {
        Spawning(MainZone);
        
        // Spawning(AZone);
        // Spawning(BZone);
        // Spawning(CZone);
        // Spawning(BossZone);
    }

    public void SpawnAZone() { Spawning(AZone); }
    public void SpawnBZone() { Spawning(BZone); }
    public void SpawnCZone() { Spawning(CZone); }
    public void SpawnBossZone() { Spawning(BossZone); }

    private void Spawning(List<Spawn> zones)
    {
        if (zones == null)
        {
            Debug.Log("Unvalid zone");
            return;
        }

        float positionX;
        float positionZ;

        foreach (var zone in zones)
        {
            for (int i = 0; i < zone.GetSpawnMeleeZombieAmount(); i++)
            {
                Zombie zombie = ZombieManager.Instance.GetZombieByID(100);
                positionX = Random.Range(zone.GetTopLeftTransform().position.x, zone.GetRightBottomTransform().position.x);
                positionZ = Random.Range(zone.GetTopLeftTransform().position.z, zone.GetRightBottomTransform().position.z);
                zombie.transform.position = new Vector3(positionX, zombie.transform.position.y, positionZ);

                SetWayPoint(zombie, zone.GetWayPoints());

                //Debug.Log("Spawn zombie at" + zombie.transform.position);
            }
            for (int i = 0; i < zone.GetSpawnRangeZombieAmount(); i++)
            {
                Zombie zombie = ZombieManager.Instance.GetZombieByID(200);
                positionX = Random.Range(zone.GetTopLeftTransform().position.x, zone.GetRightBottomTransform().position.x);
                positionZ = Random.Range(zone.GetTopLeftTransform().position.z, zone.GetRightBottomTransform().position.z);
                zombie.transform.position = new Vector3(positionX, zombie.transform.position.y, positionZ);

                SetWayPoint(zombie, zone.GetWayPoints());

                //Debug.Log("Spawn zombie at" + zombie.transform.position);
            }
        }
    }

    private void SetWayPoint(Zombie zombie, Transform wayPoints)
    {
        NavigationAI navigationAI = zombie.GetComponent<NavigationAI>();

        if (navigationAI != null)
        {
            foreach (Transform wayPoint in wayPoints.transform)
            {
                navigationAI.AddWayPoint(wayPoint);

                //Debug.Log(wayPoint.name);
            }
        }

        NavMeshAgent agent = zombie.GetComponent<NavMeshAgent>();
        if (agent.isOnNavMesh == false)
        {
            //zombie.transform.position -= new Vector3(0, 0.01f, 0);

            agent.enabled = false;
            agent.enabled = true;

            // NavMeshHit hit;
            // if (NavMesh.SamplePosition(zombie.transform.position, out hit, 1f, NavMesh.AllAreas) == true)
            // {
            //     zombie.transform.position = hit.position;
            // }
        }
    }
}