using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private Outline outline;

    private void Start()
    {
        // outline = ItemManager.Instance.GetOutline();
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void Interact()
    {
        Item item = this.GetComponent<Item>();

        if (item != null)
        {
            gameObject.SetActive(false);
            InventoryUIManager.Instance.AddItem(item);
        }

        else
        {
            CardReader cardReader = GetComponent<CardReader>();
            if (cardReader.GetIsOpen() == false)
            {
                foreach (var zoneName in PlayerManager.Instance.GetZoneNames())
                {
                    if (zoneName == cardReader.GetConnectedZoneName())
                    {
                        cardReader.SetIsOpen(true);
                        cardReader.GetComponent<Outline>().OutlineColor = new Color(0.341f, 1f, 0f);
                        return;
                    }
                }
                Debug.Log("Something wrong");
            }
        }
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }
}
