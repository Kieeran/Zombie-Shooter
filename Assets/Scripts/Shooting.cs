using System.Collections.Generic;
using InputSystem;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private GunConfig currentGunConfig;
    private float Counter;
    private bool isPressed;
    private Vector3 mouseWorldPosition;
    private CameraController cameraController;

    private PlayerInputSystem playerInputSystem;
    private void Awake()
    {
        cameraController = GetComponent<CameraController>();
    }

    private void Start()
    {
        playerInputSystem = PlayerManager.Instance.GetPlayerInputSystem();
        currentGunConfig = PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig();
        Counter = 0f;
    }

    private void OnShoot()
    {
        if (UIManager.Instance.GetIsReloadingUI() == true) return;
        if (PlayerManager.Instance.GetInventory().IsMagazineEmpty(PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun()) == true) return;

        if (currentGunConfig != null && currentGunConfig.Automatic == true)
        {
            if (playerInputSystem.shoot == true)
            {
                if (Counter > currentGunConfig.FireCoolDown)
                {
                    Counter = 0f;
                    ShootBullet();
                }
            }
        }

        else
        {
            if (playerInputSystem.shoot == true && isPressed == false)
            {
                isPressed = true;
                if (Counter > currentGunConfig.FireCoolDown)
                {
                    Counter = 0f;
                    ShootBullet();
                }
            }

            if (playerInputSystem.shoot == false) isPressed = false;
        }

        Counter += Time.deltaTime;
        //Debug.Log(playerInputSystem.shoot);
    }

    private Vector3 forceDirection = Vector3.zero;

    private void ShootBullet()
    {
        Bullet bullet = BulletManager.Instance.GetBullet();
        //Transform bulletSpawnPoint = PlayerManager.Instance.GetCurrentGun().GetBulletSpawnPoint();
        Transform bulletSpawnPoint = PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetBulletSpawnPoint();
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.gameObject.SetActive(true);

        mouseWorldPosition = cameraController.GetMouseWorldPosition();
        forceDirection = (mouseWorldPosition - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Rigidbody>().AddForce(forceDirection * currentGunConfig.BulletSpeed, ForceMode.Impulse);
        bullet.StartCountingToDisappear();

        PlayerManager.Instance.GetInventory().UpdateAmmoInventory();
    }

    private void LateUpdate()
    {
        //currentGunConfig = PlayerManager.Instance.GetCurrentGun().GetGunConfig();
        currentGunConfig = PlayerManager.Instance.GetPlayerWeapon().GetCurrentGun().GetGunConfig();
    }

    private void FixedUpdate()
    {
        OnShoot();
    }
}
