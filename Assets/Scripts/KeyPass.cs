using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPass : MonoBehaviour
{
    private bool IsKeyCollected;

    public bool GetIsKeyCollected() { return IsKeyCollected; }
    public void SetIsKeyCollected(bool value)
    {
        IsKeyCollected = true;
    }
}
