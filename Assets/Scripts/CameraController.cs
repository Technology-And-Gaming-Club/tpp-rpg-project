using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float _distanceFromTarget = 3.0f;
    public Transform target;
    public Transform camTrans;
    public float mouseSensitivity;
    public bool invertX;
    public bool invertY;
    public float rlerp = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - transform.forward* _distanceFromTarget;
        transform.rotation = Quaternion.Lerp(transform.rotation, camTrans.rotation, rlerp);
        //control camera rotation

        Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
        if (invertX)
        {
            mouseInput.x = -mouseInput.x;
        }

        if (invertY)
        {
            mouseInput.y = -mouseInput.y;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);
        camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(-mouseInput.y, 0f, 0f));
    }
}


