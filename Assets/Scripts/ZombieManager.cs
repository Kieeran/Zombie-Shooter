using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;
    }

    [SerializeField] private MeleeZombie prefabMeleeZombie;
    [SerializeField] private RangeZombie prefabRangeZombie;
    [SerializeField] private BossZombie prefabBossZombie;

    private Queue<Zombie> meleeZombies;
    private Queue<Zombie> rangeZombies;

    [SerializeField] private int poolingAmount;

    private void Start()
    {
        InitPooling();
    }

    public void InitPooling()
    {
        InitMeleeZombies();
        InitRangeZombies();
    }

    private void InitMeleeZombies()
    {
        if (meleeZombies == null) meleeZombies = new();

        for (int i = 0; i < poolingAmount; i++)
        {
            Zombie zombie = Instantiate(prefabMeleeZombie);

            //zombie.transform.SetParent(transform);

            zombie.gameObject.SetActive(false);

            meleeZombies.Enqueue(zombie);
        }
    }

    private void InitRangeZombies()
    {
        if (rangeZombies == null) rangeZombies = new();

        for (int i = 0; i < poolingAmount / 2; i++)
        {
            Zombie zombie = Instantiate(prefabRangeZombie);

            //zombie.transform.SetParent(transform);

            zombie.gameObject.SetActive(false);

            rangeZombies.Enqueue(zombie);
        }
    }

    public Zombie GetZombieByID(int ID)
    {
        Zombie zombie = null;
        switch (ID)
        {
            case 100:           // Melee Zombie
                if (meleeZombies.Count <= 0)
                {
                    InitMeleeZombies();
                }
                zombie = meleeZombies.Dequeue();
                break;
            case 200:           // Range Zombie
                if (rangeZombies.Count <= 0)
                {
                    InitRangeZombies();
                }
                zombie = rangeZombies.Dequeue();

                break;
            case 300:           // Boss Zombie
                zombie = Instantiate(prefabBossZombie);

                break;
            default:
                Debug.Log("Unvalid ID");
                break;
        }

        if (zombie == null)
        {
            Debug.Log("Something wrong with Zombie Manager");
            return null;
        }

        zombie.gameObject.SetActive(true);

        //NavMeshHit hit;
        // if (NavMesh.SamplePosition(zombie.transform.position, out hit, 1f, NavMesh.AllAreas) == true)
        // {
        //     zombie.transform.position = hit.position;
        // }

        // zombie.GetComponent<NavMeshAgent>().enabled = true;
        // zombie.GetComponent<NavigationAI>().enabled = true;
        //zombie.transform.SetParent(null);
        return zombie;
    }

    // public void ReturnZombie(Zombie zombie)
    // {
    //     zombie.transform.SetParent(transform);
    //     zombie.gameObject.SetActive(false);

    //     if (zombie.GetZombieConfig().ZombieID == 100)
    //     {
    //         meleeZombies.Enqueue(zombie);
    //     }
    //     else if (zombie.GetZombieConfig().ZombieID == 200)
    //     {
    //         rangeZombies.Enqueue(zombie);
    //     }

    //     // zombie.GetComponent<NavMeshAgent>().enabled = false;
    //     // zombie.GetComponent<NavigationAI>().enabled = false;
    // }
}