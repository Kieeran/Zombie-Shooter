using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.TryGetComponent(out AutoDoor door))
        {
            if (collider.tag == "DoorNeedKey")
            {
                CardReader cardReader = door.transform.Find("Card Reader").GetComponent<CardReader>();
                if (cardReader != null && cardReader.GetIsOpen() == false) return;
            }
            door.OpenDoor();
        }
    }
}