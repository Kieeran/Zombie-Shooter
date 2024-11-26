using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Item item;

    public void SetItem(Item item)
    {
        this.item = item;
        Image itemImage = transform.Find("ItemIcon").GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = item.GetItemConfig().ItemIcon;
            itemImage.gameObject.SetActive(true);
        }
    }

    public Item GetItem() { return item; }
}
