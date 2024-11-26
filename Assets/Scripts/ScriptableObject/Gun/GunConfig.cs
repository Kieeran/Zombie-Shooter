using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gun/GunConfig", fileName = "GunConfig")]
public class GunConfig : ScriptableObject
{
    [field: SerializeField] public int WeaponID { get; private set; }
    [field: SerializeField] public float FireCoolDown { get; private set; }
    [field: SerializeField] public float BulletSpeed { get; private set; }
    [field: SerializeField] public Vector3 AimPosition { get; private set; }
    [field: SerializeField] public Vector3 NormalPosition { get; private set; }
    [field: SerializeField] public bool Automatic { get; private set; }
    [field: SerializeField] public float HeadDamage { get; private set; }
    [field: SerializeField] public float BodyDamage { get; private set; }
    [field: SerializeField] public Sprite GunIcon { get; private set; }
    [field: SerializeField] public int MagazineCapacity { get; private set; }
}