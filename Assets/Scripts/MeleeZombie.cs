using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeZombie : Zombie
{
    private Transform player;

    [SerializeField] private float maxAttackDistance;

    protected override void Start()
    {
        base.Start();

        player = PlayerManager.Instance.GetPlayerTransform();
    }

    public void CheckEnemyHitPlayer()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= maxAttackDistance)
        {
            //Debug.Log("Zombie hit Player");
            //Debug.Log(Vector3.Distance(player.transform.position, transform.position));

            PlayerManager.Instance.PlayerTakeDamage(zombieConfig.Damage);
            Debug.Log(zombieConfig.Damage);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
