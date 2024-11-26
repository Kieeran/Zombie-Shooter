using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Player/PlayerConfig", fileName = "PlayerConfig")]
public class PlayerConfig : CharacterConfig
{
    [field: SerializeField] public int Shield { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }

    [Tooltip("0: Assault Rifle; 1: Grenade Laucher; 2: Pistol")]
    [field: SerializeField] public int StartingWeaponID { get; private set; }
    [field: SerializeField] public List<int> StartingWeapons { get; private set; }

    [field: SerializeField] public List<int> WeaponAmmoStart { get; private set; }
}