using InputSystem;
using UnityEngine;

public class AnimateWeapon : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private FirstPersonController firstPersonController;
    private PlayerInputSystem playerInputSystem;

    private void Start()
    {
        playerInputSystem = PlayerManager.Instance.GetPlayerInputSystem();
        firstPersonController = PlayerManager.Instance.GetFirstPersonController();
    }

    private void Update()
    {
        // if (firstPersonController.GetSpeed() > 0f)
        // {
        //     animator.SetBool("IsHide", true);
        // }

        // else
        // {
        //     animator.SetBool("IsHide", false);
        // }

        if (playerInputSystem.reload == true && animator.GetBool("IsReload") == false && animator.GetBool("IsReady") == false && animator.GetBool("IsShoot") == false)
        {
            animator.SetBool("IsReload", true);
            playerInputSystem.reload = false;
        }

        if (playerInputSystem.shortcut1 == true && animator.GetBool("IsReload") == false && animator.GetBool("IsReady") == false && animator.GetBool("IsShoot") == false)
        {
            animator.SetBool("IsReady", true);
            playerInputSystem.shortcut1 = false;
        }
        if (animator.GetBool("IsReload") == false && animator.GetBool("IsReady") == false)
            animator.SetBool("IsShoot", playerInputSystem.shoot);
    }

    public void DoneReload()
    {
        animator.SetBool("IsReload", false);
    }

    public void DoneReady()
    {
        animator.SetBool("IsReady", false);
    }
}