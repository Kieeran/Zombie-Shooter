using System.Collections.Generic;
using InputSystem;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    //[SerializeField] private Animator animator;
    [SerializeField] private FirstPersonController firstPersonController;
    [SerializeField] private PlayerInputSystem playerInputSystem;
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private Inventory inventory;

    [SerializeField] private bool IsWin;
    [SerializeField] private bool IsLose;

    public void SetIsWin()
    {
        IsWin = true;
        UIManager.Instance.PlayeWinAnimation();
    }
    public void SetIsLose()
    {
        IsLose = true;
    }

    private float HP;
    private float shield;

    public float GetPlayerHP() { return HP; }
    public void AddPlayerHP(int HP) { this.HP += HP; }
    public void PlayerTakeDamage(float damage)
    {
        UIManager.Instance.PlayDamageAnimation();
        if (HP == 0) return;

        if (shield > 0)
        {
            shield -= damage;

            if (shield < 0)
            {
                HP -= shield;
                shield = 0;
            }
        }

        else
        {
            HP -= damage;
        }


        if (HP <= 0)
        {
            HP = 0;
            Debug.Log("Player is dead");
            UIManager.Instance.PlayeLoseAnimation();
        }
        UIManager.Instance.SetHealthUI(HP);
        UIManager.Instance.SetShieldUI(shield);
    }

    public float GetPlayerShield() { return shield; }
    public void AddPlayerShield(int shield) { this.shield += shield; }

    public Transform GetPlayerTransform() { return transform; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;
    }

    private void Start()
    {
        // Set initial HP and shield to player
        HP = playerConfig.HP;
        shield = playerConfig.Shield;

        // Set initial HP and shield UI
        UIManager.Instance.SetHealthUI(HP);
        UIManager.Instance.SetShieldUI(shield);
    }

    private List<string> zoneNames = new List<string>();
    public void AddZone(string s)
    {
        zoneNames.Add(s);
    }
    public List<string> GetZoneNames() { return zoneNames; }

    public PlayerInputSystem GetPlayerInputSystem() { return playerInputSystem; }
    public FirstPersonController GetFirstPersonController() { return firstPersonController; }
    public PlayerConfig GetPlayerConfig() { return playerConfig; }
    public PlayerWeapon GetPlayerWeapon() { return playerWeapon; }
    public Inventory GetInventory() { return inventory; }
}