using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeZombie : Zombie
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

            // Ray ray = new Ray(transform.position, player.transform.position);
            // Debug.Log("Before");
            // if (Physics.Raycast(ray, out RaycastHit hitInfo, maxAttackDistance))
            // {
            //     Debug.Log("After");
            //     if (hitInfo.collider.tag == "Player")
            //     {
            //         Debug.Log("Final");
            //         PlayerManager.Instance.PlayerTakeDamage(zombieConfig.Damage);
            //         Debug.Log(zombieConfig.Damage);
            //     }
            // }

            PlayerManager.Instance.PlayerTakeDamage(zombieConfig.Damage);
            Debug.Log(zombieConfig.Damage);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
