using System.Collections;
using System.Collections.Generic;
using InputSystem;
using Unity.Mathematics;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;
    }

    [SerializeField] private List<Gun> guns;

    public Gun GetGunByID(int ID)
    {
        return Instantiate(guns[ID]);
    }

    public void ResetGunTransform(Gun gun)
    {
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;
        gun.transform.localScale = Vector3.one;
    }
}
