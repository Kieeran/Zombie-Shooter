using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    [SerializeField] private string connectedZoneName;
    public string GetConnectedZoneName() { return connectedZoneName; }

    private bool isOpen = false;

    public bool GetIsOpen() { return isOpen; }
    public void SetIsOpen(bool value)
    {
        isOpen = value;
    }
}