using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemConfig itemConfig;
    private bool IsItemUsed = false;

    public bool GetIsItemUsed() { return IsItemUsed; }
    public void SetIsItemUsed(bool value) { IsItemUsed = value; }

    public ItemConfig GetItemConfig() { return itemConfig; }
}
