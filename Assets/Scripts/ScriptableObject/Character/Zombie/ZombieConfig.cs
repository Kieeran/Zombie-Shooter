using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Zombie/ZombieConfig", fileName = "ZombieConfig")]
public class ZombieConfig : CharacterConfig
{
    [field: SerializeField] public int ZombieID { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float AttackSpeed { get; private set; }
}