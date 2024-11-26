using Unity.VisualScripting;
using UnityEngine;

public class EnterWinZone : MonoBehaviour
{
    [SerializeField] private Transform winZone;
    private Transform LeftTop;
    private Transform RightBottom;

    private void Start()
    {
        LeftTop = winZone.transform.Find("LeftTop");
        RightBottom = winZone.transform.Find("RightBottom");
    }

    private void Update()
    {
        if (transform.position.x >= RightBottom.position.x && transform.position.x <= LeftTop.position.x &&
            transform.position.z >= RightBottom.position.z && transform.position.z <= LeftTop.position.z)
        {
            PlayerManager.Instance.SetIsWin();
        }
    }
}