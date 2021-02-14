using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float rotate_speed = 5f;
    public float rotate_h;
    public float rotate_v;
    public float cam_y;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the position of the mouse & rotate the target
        rotate_h = Input.GetAxis("Mouse X") * rotate_speed;
        rotate_v += Input.GetAxis("Mouse Y") * rotate_speed;
        rotate_v = Mathf.Clamp(rotate_v, -50, 20);

        //Move the camera based on the current rotation of the target & the original offset
        target.Rotate(0, rotate_h, 0);
        cam_y = target.eulerAngles.y;

        Quaternion cam_rotate = Quaternion.Euler(-rotate_v, cam_y, 0);
        transform.position = target.position - (cam_rotate * offset);
        transform.LookAt(target);
    }
}
