using System.Collections.Generic;
using InputSystem;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private PlayerInputSystem playerInputSystem;
    [SerializeField] private GameObject UI_Inventory;

    [SerializeField] private GameObject keys;

    public bool IsOutOfAmmo(Gun gun)
    {
        return gun.GetTotalAmmo() <= 0;
    }

    public bool IsMagazineEmpty(Gun gun)
    {
        return gun.GetCurrentMagazineAmmo() <= 0;
    }

    private Gun currentGun;

    public List<Item> items = new();

    public bool GetIsInventoryOpen() { return isInventoryOpen; }
    private bool isInventoryOpen = false;
    private void Start()
    {
        playerInputSystem = PlayerManager.Instance.GetPlayerInputSystem();
        UI_Inventory.gameObject.SetActive(false);

        PlayerConfig playerConfig = PlayerManager.Instance.GetPlayerConfig();

        // Set initial total ammo to each type of gun available
        List<Gun> availableGuns = PlayerManager.Instance.GetPlayerWeapon().GetAvailableGuns();
        for (int i = 0; i < playerConfig.WeaponAmmoStart.Count; i++)
        {
            availableGuns[i].SetTotalAmmo(playerConfig.WeaponAmmoStart[i]);
            Reload(availableGuns[i]);
        }

        currentGun = PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun();

        UIManager.Instance.UpdateBulletsHud(currentGun.GetCurrentMagazineAmmo(), currentGun.GetTotalAmmo());
    }

    private void Update()
    {
        OpenNCloseInventory();
    }

    public void UpdateAmmoInventory()
    {
        currentGun = PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun();
        currentGun.SetCurrentMagazineAmmo(currentGun.GetCurrentMagazineAmmo() - 1);

        UIManager.Instance.SetCurrentMagazineAmmoText(currentGun.GetCurrentMagazineAmmo());
    }

    public void AddItem(Item item)
    {
        items.Add(item);

        if (item.GetItemConfig().ItemID == 1000)        // Key A
        {
            PlayerManager.Instance.AddZone("ZoneA");
            SpawnManager.Instance.SpawnAZone();
        }

        else if (item.GetItemConfig().ItemID == 1010)        // Key B
        {
            PlayerManager.Instance.AddZone("ZoneB");
            SpawnManager.Instance.SpawnBZone();
        }

        else if (item.GetItemConfig().ItemID == 1020)        // Key C
        {
            PlayerManager.Instance.AddZone("ZoneC");
            SpawnManager.Instance.SpawnCZone();
            SpawnManager.Instance.SpawnBossZone();
        }

        else if (item.GetItemConfig().ItemID == 1080)        // Key Exit
        {
            PlayerManager.Instance.AddZone("Exit");
        }
    }

    public void UseItem(Item item)
    {
        ItemConfig itemConfig = item.GetItemConfig();

        if (itemConfig.ItemID == 1000)              // Key A
        {
            Debug.Log("Use Key A");
            //item.SetIsItemUsed(true);
        }

        else if (itemConfig.ItemID == 1010)             // Key B
        {
            Debug.Log("Use Key B");
            //item.SetIsItemUsed(true);
        }
        else if (itemConfig.ItemID == 1020)             // Key C
        {
            Debug.Log("Use Key C");
            //item.SetIsItemUsed(true);
        }

        else if (itemConfig.ItemID == 1080)             // Key Exit
        {
            Debug.Log("Use Key Exit");
            //item.SetIsItemUsed(true);
        }

        else if (itemConfig.ItemID == 1030)             // Medical Kit
        {
            Debug.Log("Use Medical Kit");

            PlayerManager.Instance.AddPlayerHP(itemConfig.Value);
            UIManager.Instance.SetHealthUI(PlayerManager.Instance.GetPlayerHP());

            item.SetIsItemUsed(true);
        }

        else if (itemConfig.ItemID == 1070)             // Shield
        {
            Debug.Log("Use Shield");

            PlayerManager.Instance.AddPlayerShield(itemConfig.Value);
            UIManager.Instance.SetShieldUI(PlayerManager.Instance.GetPlayerShield());

            item.SetIsItemUsed(true);
        }

        else if (itemConfig.ItemID == 1040 || itemConfig.ItemID == 1050 || itemConfig.ItemID == 1060)             // Ammo Box
        {
            Debug.Log("Use Ammo Box have ID: " + itemConfig.ItemID);
            foreach (Gun gun in PlayerManager.Instance.GetPlayerWeapon().GetAvailableGuns())
            {
                Debug.Log("Gun ID " + gun.GetGunConfig().WeaponID);
                if (gun.GetGunConfig().WeaponID == 0 && itemConfig.ItemID == 1040)          // Assault Rifle ID
                {
                    gun.SetTotalAmmo(gun.GetTotalAmmo() + itemConfig.Value);
                    item.SetIsItemUsed(true);

                    if (PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().WeaponID == gun.GetGunConfig().WeaponID)
                    {
                        UIManager.Instance.SetTotalAmmoText(gun.GetTotalAmmo());
                    }

                    return;
                }

                else if (gun.GetGunConfig().WeaponID == 1 && itemConfig.ItemID == 1050)     // Grenade Laucher ID
                {
                    gun.SetTotalAmmo(gun.GetTotalAmmo() + itemConfig.Value);
                    item.SetIsItemUsed(true);

                    if (PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().WeaponID == gun.GetGunConfig().WeaponID)
                    {
                        UIManager.Instance.SetTotalAmmoText(gun.GetTotalAmmo());
                    }

                    return;
                }

                else if (gun.GetGunConfig().WeaponID == 2 && itemConfig.ItemID == 1060)     // Pistol ID
                {
                    gun.SetTotalAmmo(gun.GetTotalAmmo() + itemConfig.Value);
                    item.SetIsItemUsed(true);

                    if (PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig().WeaponID == gun.GetGunConfig().WeaponID)
                    {
                        UIManager.Instance.SetTotalAmmoText(gun.GetTotalAmmo());
                    }

                    return;
                }

                else
                {
                    Debug.Log("Don't have Gun match with Ammo Bullet");
                    item.SetIsItemUsed(false);
                }
            }
        }
    }

    public void DropItem(Item item)
    {
        ItemConfig itemConfig = item.GetItemConfig();

        if (itemConfig.ItemID == 1000)              // Key A
        {
            Debug.Log("Drop Key A");
        }

        else if (itemConfig.ItemID == 1010)             // Key B
        {
            Debug.Log("Drop Key B");
        }
        else if (itemConfig.ItemID == 1020)             // Key C
        {
            Debug.Log("Drop Key C");
        }

        else if (itemConfig.ItemID == 1080)             // Key Exit
        {
            Debug.Log("Drop Key Exit");
        }

        // else if (itemConfig.ItemID == 1030)             // Medical Kit
        // {
        //     Debug.Log("Drop Medical Kit");
        // }

        // else if (itemConfig.ItemID == 1070)             // Shield
        // {
        //     Debug.Log("Drop Medical Kit");
        // }

        // else if (itemConfig.ItemID == 1040)             // Grenade Laucher Ammo Box
        // {
        //     Debug.Log("Drop Grenade Laucher Ammo Box");
        // }

        // else if (itemConfig.ItemID == 1050)             // Pistol Ammo Box
        // {
        //     Debug.Log("Drop Pistol Ammo Box");
        // }
        // else if (itemConfig.ItemID == 1060)             // Rifle Ammo Box
        // {
        //     Debug.Log("Drop Rifle Ammo Box");
        // }

        Transform playerTransform = PlayerManager.Instance.GetPlayerTransform();
        Ray ray = new Ray(playerTransform.position, -playerTransform.up);

        // Debug.Log("Before");

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 10f))
        {
            item.transform.position = raycastHit.point;
            //item.transform.position = playerTransform.position;
            item.gameObject.SetActive(true);
            //Debug.Log("After");
        }
    }

    public void Reload(Gun gun)
    {
        int currentMagazineAmmo = gun.GetCurrentMagazineAmmo();
        int totalAmmo = gun.GetTotalAmmo();
        int magazineCapacity = gun.GetMagazineCapacity();

        if (IsOutOfAmmo(gun))
        {
            //Debug.Log("Current gun is out of ammo");
            return;
        }

        if (IsMagazineEmpty(gun))
        {
            if (totalAmmo >= magazineCapacity)
            {
                currentMagazineAmmo = magazineCapacity;
                totalAmmo -= magazineCapacity;
            }

            else
            {
                currentMagazineAmmo = totalAmmo;
                totalAmmo = 0;
            }
        }

        else
        {
            if (totalAmmo >= magazineCapacity - currentMagazineAmmo)
            {
                totalAmmo -= magazineCapacity - currentMagazineAmmo;
                currentMagazineAmmo = magazineCapacity;
            }

            else
            {
                currentMagazineAmmo += totalAmmo;
                totalAmmo = 0;
            }
        }

        gun.SetCurrentMagazineAmmo(currentMagazineAmmo);
        gun.SetTotalAmmo(totalAmmo);

        //Debug.Log("Reloading...");
    }

    private void OpenNCloseInventory()
    {
        if (playerInputSystem.openInventory == true)
        {
            isInventoryOpen = !isInventoryOpen;
            if (isInventoryOpen == true)
            {
                playerInputSystem.SetCursorState(false);
                GetComponent<FirstPersonController>().FreezeCamera(true);
                Time.timeScale = 0f;
                UI_Inventory.gameObject.SetActive(true);

            }

            else
            {
                playerInputSystem.SetCursorState(true);
                GetComponent<FirstPersonController>().FreezeCamera(false);
                Time.timeScale = 1f;
                UI_Inventory.gameObject.SetActive(false);
            }

            playerInputSystem.openInventory = false;
        }
    }
}