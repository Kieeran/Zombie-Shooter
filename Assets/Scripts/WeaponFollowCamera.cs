using System.Collections;
using System.Collections.Generic;
using InputSystem;
using UnityEngine;

public class WeaponFollowCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraRoot;
    //private Vector3 mouseWorldPosition = Vector3.zero;

    private void Update()
    {
        // Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        // Ray ray = new Ray(transform.position, transform.forward);

        // if (Physics.Raycast(ray, out RaycastHit raycastHit, 200f))
        // {
        //     mouseWorldPosition = raycastHit.point;
        // }

        // Vector3 worldAimTarget = mouseWorldPosition;
        // worldAimTarget.y = transform.position.y;

        // Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
        // Quaternion targetRotation = Quaternion.LookRotation(aimDirection);

        PlayerInputSystem playerInputSystem = PlayerManager.Instance.GetPlayerInputSystem();
        if (playerInputSystem.aim == false)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(cameraRoot.forward), Time.deltaTime * 20f);
            transform.position = Vector3.Lerp(transform.position, cameraRoot.position, Time.deltaTime * 20f);
        }
    }
}