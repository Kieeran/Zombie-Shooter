using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Item/ItemConfig", fileName = "ItemConfig")]
public class ItemConfig : ScriptableObject
{
    [field: SerializeField] public float ItemID { get; private set; }
    [field: SerializeField] public string ItemName { get; private set; }
    [field: SerializeField] public int Value { get; private set; }
    [field: SerializeField] public Sprite ItemIcon { get; private set; }

    // On Start
    // [field: SerializeField] public Mesh ItemMesh { get; private set; }
    // [field: SerializeField] public List<Material> ItemMaterials { get; private set; }
}