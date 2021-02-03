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
    public float cam_x;

    // Start is called before the first frame update
    void Start()
    {
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Get the position of the mouse & rotate the target
        rotate_h = Input.GetAxis("Mouse X") * rotate_speed;
        target.Rotate(0, rotate_h, 0);

        rotate_v = Input.GetAxis("Mouse Y") * rotate_speed;
        target.Rotate(-rotate_v, 0, 0);

        //Move the camera based on the current rotation of the target & the original offset
        cam_y = target.eulerAngles.y;
        cam_x = target.eulerAngles.x;

        Quaternion cam_rotate = Quaternion.Euler(cam_x, cam_y, 0);
        transform.position = target.position - (cam_rotate * offset);

        //transform.position = target.position - offset;

        transform.LookAt(target);
    }
}
