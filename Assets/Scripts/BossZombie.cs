using UnityEngine;
using UnityEngine.Rendering;

public class BossZombie : Zombie
{
    [SerializeField] private int hitsBeforeRageModeHit;
    [SerializeField] private int shootsBeforeRageModeShoot;
    [SerializeField] private int hitAttackRange;
    [SerializeField] private int shootAttackRange;
    [SerializeField] private int chaseRange;
    [SerializeField] private int chaseSpeed;
    [SerializeField] private float damageHitRage;
    [SerializeField] private float damageShootRage;

    private bool isMeleeOrRange;
    public bool GetIsMeleeOrRange() { return isMeleeOrRange; }
    public void ToggleIsMeleeOrRange() { isMeleeOrRange = !isMeleeOrRange; }

    public int GetHitAttackRange() { return hitAttackRange; }
    public int GetShootAttackRange() { return shootAttackRange; }
    public int GetChaseRange() { return chaseRange; }
    public int GetChaseSpeed() { return chaseSpeed; }

    private int hitcount;
    private int shootCount;

    private bool isDoneHitMode = false;
    private bool isDoneShootMode = false;

    public bool GetIsDoneHitMode() { return isDoneHitMode; }
    public void SetIsDoneHitMode(bool b) { isDoneHitMode = b; }
    public bool GetIsDoneShootMode() { return isDoneShootMode; }
    public void SetIsDoneShootMode(bool b) { isDoneShootMode = b; }

    [SerializeField] private Material normalStageMaterial;
    [SerializeField] private Material rageStageMaterial;

    public int GetHitCount() { return hitcount; }
    public int GetShootCount() { return shootCount; }
    public void IncrementHitCount()
    {
        hitcount++;
        CheckEnemyHitPlayer();
    }
    public void IncrementShootCount()
    {
        shootCount++;
        CheckEnemyShootPlayer();
    }

    bool isDropKey = false;

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (isDropKey == true) return;
        Ray ray = new Ray(transform.position, -transform.up);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f))
        {
            Item keyExit = ItemManager.Instance.GetKeyExit();

            keyExit.transform.position = raycastHit.point;
            isDropKey = true;
            //item.transform.position = playerTransform.position;
            //keyExit.gameObject.SetActive(true);
        }
    }

    // protected override void StartCountingToDisable()
    // {
    //     base.StartCountingToDisable();

    //     Ray ray = new Ray(transform.position + new Vector3(0.01f, 0, 0), -transform.up);

    //     if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f))
    //     {
    //         Item keyExit = ItemManager.Instance.GetKeyExit();

    //         keyExit.transform.position = raycastHit.point + new Vector3(0, 0.01f, 0);
    //         //item.transform.position = playerTransform.position;
    //         //keyExit.gameObject.SetActive(true);
    //     }

    // }

    public void ResetHitCount() { hitcount = 0; }
    public void ResetShootCount() { shootCount = 0; }

    protected override void Start()
    {
        base.Start();

        ChangeColor(normalStageMaterial);
        isMeleeOrRange = true;
    }

    private void ChangeColor(Material material)
    {
        foreach (Transform child in gameObject.transform.Find("Armour"))
        {
            SkinnedMeshRenderer renderer = child.GetComponent<SkinnedMeshRenderer>();
            if (renderer != null)
            {
                renderer.material = material;
            }
        }
    }

    public void CheckEnemyHitPlayer()
    {
        Transform player = PlayerManager.Instance.GetPlayerTransform();
        if (Vector3.Distance(player.transform.position, transform.position) <= hitAttackRange)
        {
            //Debug.Log("Zombie hit Player");
            //Debug.Log(Vector3.Distance(player.transform.position, transform.position));

            float damage = zombieConfig.Damage;
            if (hitcount == hitsBeforeRageModeHit + 1)
            {
                damage = damageHitRage;
            }

            PlayerManager.Instance.PlayerTakeDamage(damage);
            Debug.Log(damage);
        }
    }

    public void CheckEnemyShootPlayer()
    {
        Transform player = PlayerManager.Instance.GetPlayerTransform();
        if (Vector3.Distance(player.transform.position, transform.position) <= shootAttackRange)
        {
            //Debug.Log("Zombie hit Player");
            //Debug.Log(Vector3.Distance(player.transform.position, transform.position));

            float damage = zombieConfig.Damage + 20;
            if (shootCount == shootsBeforeRageModeShoot + 1)
            {
                damage = damageShootRage;
            }

            PlayerManager.Instance.PlayerTakeDamage(damage);
            Debug.Log(damage);
        }
    }

    void Update()
    {
        if (isMeleeOrRange == true)
        {
            if (hitcount == hitsBeforeRageModeHit)
            {
                ChangeColor(rageStageMaterial);
            }

            if (hitcount == hitsBeforeRageModeHit + 1)
            {
                ChangeColor(normalStageMaterial);

                hitcount = 0;
                isMeleeOrRange = !isMeleeOrRange;

                Animator animator = GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("IsAttackingMelee", false);
                    animator.SetBool("IsChasing", true);
                    //animator.SetBool("IsMeleeOrRange", isMeleeOrRange);
                }
                //isDoneHitMode = true;
            }
        }

        else
        {
            if (shootCount == shootsBeforeRageModeShoot)
            {
                ChangeColor(rageStageMaterial);
            }

            if (shootCount == shootsBeforeRageModeShoot + 1)
            {
                ChangeColor(normalStageMaterial);

                shootCount = 0;
                isMeleeOrRange = !isMeleeOrRange;

                Animator animator = GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("IsAttackingRange", false);
                    animator.SetBool("IsChasing", true);
                    //animator.SetBool("IsMeleeOrRange", isMeleeOrRange); 
                }
                //isDoneShootMode = true;
            }
        }
    }
}