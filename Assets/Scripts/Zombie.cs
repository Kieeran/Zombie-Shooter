using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] protected ZombieConfig zombieConfig;

    protected float HP;
    protected float walkSpeed;
    protected float runSpeed;
    protected float damage;
    protected float attackSpeed;

    protected virtual void Start()
    {
        HP = zombieConfig.HP;
        walkSpeed = zombieConfig.WalkSpeed;
        runSpeed = zombieConfig.RunSpeed;
        damage = zombieConfig.Damage;
        attackSpeed = zombieConfig.AttackSpeed;
    }

    public virtual ZombieConfig GetZombieConfig() { return zombieConfig; }

    public virtual void TakeDamage(float damage)
    {
        if (HP == 0) return;

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            Animator animator = GetComponent<Animator>();
            animator.SetBool("IsDead", true);
            Invoke("StartCountingToDisable", 5f);
        }
    }

    protected virtual void StartCountingToDisable()
    {
        //ZombieManager.Instance.ReturnZombie(this);
        Destroy(gameObject);
    }
}