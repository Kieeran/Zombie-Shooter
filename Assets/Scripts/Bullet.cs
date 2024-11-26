using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRb;
    [SerializeField] private TrailRenderer bulletTrail;

    private float headDamage;
    private float bodyDamage;

    public void StartCountingToDisappear()
    {
        Invoke("DisableBullet", 2f);

        if (PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().WeaponID == 1f) // Grenade Laucher
        {
            bulletRb.useGravity = true;
        }
    }

    private void DisableBullet()
    {
        bulletRb.velocity = Vector3.zero;
        bulletRb.angularVelocity = Vector3.zero;
        bulletRb.useGravity = false;

        bulletTrail.Clear();
        BulletManager.Instance.ReturnBullet(this);
    }

    public GameObject explosionEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interactable") return;

        if (PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().WeaponID != 1)          // Rifle, Pistol Gun
        {
            DisableBullet();
            Transform parent = other.transform.parent;

            headDamage = PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().HeadDamage;
            bodyDamage = PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().BodyDamage;

            if (parent.TryGetComponent(out MeleeZombie meleeZombie))
            {
                if (other.gameObject.name == "Head")
                {
                    meleeZombie.TakeDamage(headDamage);
                    Debug.Log(parent.name + " take " + headDamage + " damage from hit shot");
                }
                else if (other.gameObject.name == "Body")
                {
                    meleeZombie.TakeDamage(bodyDamage);
                    Debug.Log(parent.name + " take " + bodyDamage + " damage from body shot");
                }
            }

            if (parent.TryGetComponent(out RangeZombie rangeZombie))
            {
                if (other.gameObject.name == "Head")
                {
                    rangeZombie.TakeDamage(headDamage);
                    Debug.Log(parent.name + " take " + headDamage + " damage from hit shot");
                }
                else if (other.gameObject.name == "Body")
                {
                    rangeZombie.TakeDamage(bodyDamage);
                    Debug.Log(parent.name + " take " + bodyDamage + " damage from body shot");
                }
            }

            if (parent.TryGetComponent(out BossZombie bossZombie))
            {
                if (other.gameObject.name == "Head" || other.gameObject.name == "Head_2")
                {
                    bossZombie.TakeDamage(headDamage);
                    Debug.Log(parent.name + " take " + headDamage + " damage from hit shot");
                }
                else if (other.gameObject.name == "Body" || other.gameObject.name == "Body_2")
                {
                    bossZombie.TakeDamage(bodyDamage);
                    Debug.Log(parent.name + " take " + bodyDamage + " damage from body shot");
                }
            }
            else return;
        }

        else                                                                                                // Grenade Laucher
        {
            Debug.Log("Grenade bullet explose");
            GameObject expEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
            //KnockBack();

            CheckSurroundingCollisions();

            Destroy(expEffect, 3f);
            DisableBullet();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().WeaponID == 1)          // Grenade                                                                                              // Grenade Laucher
        {
            Debug.Log("Grenade bullet explose");
            GameObject expEffect = Instantiate(explosionEffect, transform.position, transform.rotation);
            //KnockBack();

            CheckSurroundingCollisions();

            Destroy(expEffect, 3f);
            DisableBullet();
        }
    }

    private void CheckSurroundingCollisions()
    {
        float radius = 5f;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        if (colliders == null) return;

        foreach (Collider nearby in colliders)
        {
            if (nearby == null) continue;

            Debug.Log(nearby.name);
            MeleeZombie meleeZombie = nearby.transform.parent.GetComponent<MeleeZombie>();
            RangeZombie rangeZombie = nearby.transform.parent.GetComponent<RangeZombie>();
            BossZombie bossZombie = nearby.transform.parent.GetComponent<BossZombie>();

            if (meleeZombie != null)
            {
                //meleeZombie.TakeDamage(PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().HeadDamage);

                if (Vector3.Distance(transform.position, meleeZombie.transform.position) <= radius / 2)
                    meleeZombie.TakeDamage(PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().HeadDamage);

                else
                    meleeZombie.TakeDamage(PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().HeadDamage / 2);

            }

            else if (rangeZombie != null)
            {
                if (Vector3.Distance(transform.position, rangeZombie.transform.position) <= radius / 2)
                    rangeZombie.TakeDamage(PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().HeadDamage);

                else
                    rangeZombie.TakeDamage(PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().HeadDamage / 2);

            }

            else if (bossZombie != null)
            {
                if (Vector3.Distance(transform.position, bossZombie.transform.position) <= radius / 2)
                    bossZombie.TakeDamage(PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().HeadDamage);

                else
                    bossZombie.TakeDamage(PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().HeadDamage / 2);

            }
        }
    }

    // private void KnockBack()
    // {
    //     Collider[] colliders = Physics.OverlapSphere(transform.position, 3f);

    //     foreach (Collider collider in colliders)
    //     {
    //         Debug.Log(collider.name);
    //         Rigidbody rb = collider.GetComponent<Rigidbody>();

    //         if (rb != null)
    //         {
    //             rb.AddExplosionForce(1000f, transform.position, 3);
    //         }
    //     }
    // }
}