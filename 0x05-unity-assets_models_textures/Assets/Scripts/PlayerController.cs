using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float move_speed = 20f;
    public float move_h;
    public float move_v;
    public float move_y;
    public float gravity = 9f;
    public float jump_speed = 3f;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        move_h = Input.GetAxis("Horizontal");
        move_v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(move_h, 0, move_v);


        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            move_y = jump_speed;
        }

        if (move_y >= -30)
            move_y -= gravity * Time.deltaTime;
        direction.y = move_y;

        controller.Move(direction * move_speed * Time.deltaTime);
    }
}
