using InputSystem;
using UnityEngine;

public class Interacting : MonoBehaviour
{
    [SerializeField] private float playerReach;
    private Interactable currentInteractable;
    private PlayerInputSystem playerInputSystem;

    private void Start()
    {
        playerInputSystem = PlayerManager.Instance.GetPlayerInputSystem();
    }

    private void Update()
    {
        CheckInteraction();
        if (playerInputSystem.interact == true && currentInteractable != null)
        {
            currentInteractable.Interact();

            playerInputSystem.interact = false;
        }
    }

    private void CheckInteraction()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        int layerMask = ~LayerMask.GetMask("Ignore Raycast");

        if (Physics.Raycast(ray, out RaycastHit hitInfo, playerReach, layerMask))
        {
            if (hitInfo.collider.tag == "Interactable")
            {
                Interactable newInteractable = hitInfo.collider.GetComponent<Interactable>();

                if (currentInteractable != null && newInteractable != currentInteractable)
                {
                    currentInteractable.DisableOutline();
                }

                if (newInteractable.enabled)
                {
                    SetNewcurrentInteractableItem(newInteractable);
                }

                else DisablecurrentInteractableItem();
            }
            else DisablecurrentInteractableItem();
        }

        else DisablecurrentInteractableItem();
    }

    private void SetNewcurrentInteractableItem(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
    }

    private void DisablecurrentInteractableItem()
    {
        if (currentInteractable != null)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }

    // [SerializeField] private float playerReach;
    // private Interactable currentInteractable;
    // private PlayerInputSystem playerInputSystem;

    // private void Start()
    // {
    //     playerInputSystem = PlayerManager.Instance.GetPlayerInputSystem();
    // }

    // private void Update()
    // {
    //     CheckInteraction();
    //     if (playerInputSystem.interact == true && currentInteractable != null)
    //     {
    //         currentInteractable.Interact();

    //         playerInputSystem.interact = false;
    //     }
    // }

    // private void CheckInteraction()
    // {
    //     Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

    //     if (Physics.Raycast(ray, out RaycastHit hitInfo, playerReach))
    //     {
    //         if (hitInfo.collider.tag == "Interactable")
    //         {
    //             Interactable newInteractable = hitInfo.collider.GetComponent<Interactable>();
    //             SetNewcurrentInteractableItem(newInteractable);
    //         }
    //         else DisablecurrentInteractableItem();
    //     }

    //     else DisablecurrentInteractableItem();
    // }

    // private void SetNewcurrentInteractableItem(Interactable newInteractable)
    // {
    //     currentInteractable = newInteractable;
    //     currentInteractable.EnableOutline();
    // }

    // private void DisablecurrentInteractableItem()
    // {
    //     if (currentInteractable != null)
    //     {
    //         currentInteractable.DisableOutline();
    //         currentInteractable = null;
    //     }
    // }
}