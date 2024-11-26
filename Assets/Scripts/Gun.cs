using InputSystem;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{
    [SerializeField] private GunConfig gunConfig;
    [SerializeField] private Transform bulletSpawnPoint;

    private int currentMagazineAmmo;
    private int totalAmmo;

    private bool IsCurrentMagazineEmpty = false;

    public bool GetIsCurrentMagazineEmpty() { return IsCurrentMagazineEmpty; }
    public void SetIsCurrentMagazineEmpty(bool value)
    {
        IsCurrentMagazineEmpty = value;
    }

    public int GetCurrentMagazineAmmo() { return currentMagazineAmmo; }
    public void SetCurrentMagazineAmmo(int value)
    {
        currentMagazineAmmo = value;

        if (currentMagazineAmmo <= 0)
        {
            IsCurrentMagazineEmpty = true;
        }
    }

    public int GetTotalAmmo() { return totalAmmo; }
    public void SetTotalAmmo(int value)
    {
        totalAmmo = value;
    }

    public int GetMagazineCapacity() { return gunConfig.MagazineCapacity; }

    public GunConfig GetGunConfig() { return gunConfig; }
    public Transform GetBulletSpawnPoint() { return bulletSpawnPoint; }
}