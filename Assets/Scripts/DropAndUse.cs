using UnityEngine;
using UnityEngine.EventSystems;

public class DropAndUse : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool DropItem;
    [SerializeField] private bool UseItem;
    [SerializeField] private Slot slot;
    [SerializeField] private GameObject ItemDropAndUseButton;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (UseItem)
            {
                InventoryUIManager.Instance.UseSlot(slot);
            }

            else if (DropItem)
            {
                InventoryUIManager.Instance.DropSlot(slot);
            }
        }
    }

    private void Update()
    {

    }
}