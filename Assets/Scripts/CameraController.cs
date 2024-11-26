using UnityEngine;
using Cinemachine;
using InputSystem;
public class CameraController : MonoBehaviour
{
    // [SerializeField] private float normalSensitivity;
    // [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();

    //private ThirdPersonController thirdPersonController;
    private PlayerInputSystem playerInputSystem;
    // private Animator animator;

    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform spawnBulletPosition;

    private Vector3 mouseWorldPosition = Vector3.zero;

    private void Awake()
    {
        playerInputSystem = GetComponent<PlayerInputSystem>();
        //thirdPersonController = GetComponent<ThirdPersonController>();
        //animator = GetComponent<Animator>();
    }

    public Vector3 GetMouseWorldPosition() { return mouseWorldPosition; }
    private void UpdateMouseWorldPosition()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, 200f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }
    }

    private void Update()
    {
        UpdateMouseWorldPosition();

        if (playerInputSystem.aim == true)
        {
            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;

            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(aimDirection);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        else
        {
            //thirdPersonController.SetSensitivity(normalSensitivity);
            //thirdPersonController.SetRotateOnMove(true);

            //animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 20f));
        }

        // if (playerInputSystem.shoot == true)
        // {
        //     Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
        //     Instantiate(bulletPrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDir));
        //     playerInputSystem.shoot = false;
        // }
    }
}
