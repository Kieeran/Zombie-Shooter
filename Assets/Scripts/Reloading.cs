using System.Collections;
using System.Collections.Generic;
using InputSystem;
using Unity.VisualScripting;
using UnityEngine;

public class Reloading : MonoBehaviour
{
    private PlayerInputSystem playerInputSystem;
    private Inventory inventory;
    private bool isReloadingUI;

    private void Start()
    {
        playerInputSystem = PlayerManager.Instance.GetPlayerInputSystem();
    }

    private void Update()
    {
        if (UIManager.Instance.GetIsReloadingUI() == true) return;

        inventory = PlayerManager.Instance.GetInventory();
        Gun currentGun = PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun();
        
        if (inventory.IsOutOfAmmo(currentGun)) return;

        if (playerInputSystem.reload == true && isReloadingUI != true)
        {
            inventory.Reload(currentGun);

            UIManager.Instance.StartReloadUI(currentGun.GetCurrentMagazineAmmo(), currentGun.GetTotalAmmo());

            playerInputSystem.reload = false;
        }
    }
}