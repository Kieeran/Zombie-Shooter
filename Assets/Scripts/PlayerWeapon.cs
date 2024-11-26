using System.Collections.Generic;
using InputSystem;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] PlayerInputSystem playerInputSystem;
    private List<Gun> availableGuns;
    public List<Gun> GetAvailableGuns() { return availableGuns; }

    [SerializeField] private PlayerConfig playerConfig;

    private Gun currentGun;
    public Gun GetCurrentGun() { return currentGun; }

    private int currentWeaponID;
    /*
    0: Assault Rifle
    1: Grenade Laucher
    2: Pistol
    */

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //playerConfig = PlayerManager.Instance.GetPlayerConfig();
        currentWeaponID = playerConfig.StartingWeaponID;

        availableGuns = new List<Gun>();

        // Add the available guns from the player config and store them in the availableGuns list
        foreach (var gunID in playerConfig.StartingWeapons)
        {
            Gun gun = WeaponManager.Instance.GetGunByID(gunID);
            gun.transform.SetParent(transform);
            WeaponManager.Instance.ResetGunTransform(gun);
            availableGuns.Add(gun);
        }
        SetActiveCurrentWeapon(currentWeaponID);
        UIManager.Instance.SetCurrentWeaponUI(currentWeaponID);

        currentGun = availableGuns[currentWeaponID];
    }

    private void SetActiveCurrentWeapon(int currentWeaponID)
    {
        foreach (var gun in availableGuns)
        {
            gun.gameObject.SetActive(false);
            if (availableGuns.IndexOf(gun) == currentWeaponID)
            {
                gun.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (playerInputSystem.shortcut1 == true)
        {
            currentWeaponID = 0;
            SetActiveCurrentWeapon(currentWeaponID);
            currentGun = availableGuns[currentWeaponID];

            UIManager.Instance.UpdateBulletsHud(currentGun.GetCurrentMagazineAmmo(), currentGun.GetTotalAmmo());
            UIManager.Instance.SetCurrentWeaponUI(currentWeaponID);

            playerInputSystem.shortcut1 = false;
        }

        else if (playerInputSystem.shortcut2 == true)
        {
            currentWeaponID = 1;
            SetActiveCurrentWeapon(currentWeaponID);
            currentGun = availableGuns[currentWeaponID];

            UIManager.Instance.UpdateBulletsHud(currentGun.GetCurrentMagazineAmmo(), currentGun.GetTotalAmmo());
            UIManager.Instance.SetCurrentWeaponUI(currentWeaponID);

            playerInputSystem.shortcut2 = false;
        }

        else if (playerInputSystem.shortcut3 == true)
        {
            currentWeaponID = 2;
            SetActiveCurrentWeapon(currentWeaponID);
            currentGun = availableGuns[currentWeaponID];

            UIManager.Instance.UpdateBulletsHud(currentGun.GetCurrentMagazineAmmo(), currentGun.GetTotalAmmo());
            UIManager.Instance.SetCurrentWeaponUI(currentWeaponID);

            playerInputSystem.shortcut3 = false;
        }


    }
}
