using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance;

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance);
        else
            Instance = this;
    }

    [SerializeField] private GameObject slotHolder;
    [SerializeField] private Slot prefabSlot;

    private void Start()
    {
    }

    public void AddItem(Item item)
    {
        AddSlot(item);
    }
    public void AddSlot(Item item)
    {
        Slot slot = Instantiate(prefabSlot);
        slot.transform.SetParent(slotHolder.transform, false);
        slot.SetItem(item);

        PlayerManager.Instance.GetInventory().AddItem(slot.GetItem());
    }

    public void UseSlot(Slot slot)
    {
        PlayerManager.Instance.GetInventory().UseItem(slot.GetItem());

        if (slot.GetItem().GetIsItemUsed())
            Destroy(slot.gameObject);
    }

    public void DropSlot(Slot slot)
    {
        PlayerManager.Instance.GetInventory().DropItem(slot.GetItem());
        Destroy(slot.gameObject);
    }
}