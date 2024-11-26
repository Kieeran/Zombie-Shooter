using System.Linq.Expressions;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{
    bool isOpenDoor;
    bool isCloseDoor;
    Vector3 openPosition;
    Vector3 closePosition;

    [SerializeField] private GameObject mainDoor;

    private void Start()
    {
        openPosition = Vector3.up * 5f;
        closePosition = Vector3.zero;
        isOpenDoor = false;
        isCloseDoor = false;
    }

    private void Update()
    {
        if (isOpenDoor == true)
        {
            mainDoor.transform.localPosition += new Vector3(0, 1, 0) * 0.1f;
            if (mainDoor.transform.localPosition.y > 5f)
            {
                mainDoor.transform.localPosition = openPosition;
                isOpenDoor = false;
                Invoke("CloseDoor", 1f);
            }
        }

        else if (isCloseDoor == true)
        {
            mainDoor.transform.localPosition -= new Vector3(0, 1, 0) * 0.1f;
            if (mainDoor.transform.localPosition.y < 0)
            {
                mainDoor.transform.localPosition = closePosition;
                isCloseDoor = false;
            }
        }

        //Debug.Log(mainDoor.transform.localPosition);
    }

    public void OpenDoor()
    {
        isOpenDoor = true;
    }

    public void CloseDoor()
    {
        isCloseDoor = true;
    }
}
