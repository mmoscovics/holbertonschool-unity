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
    public float jump_speed = 3.5f;

    public PauseMenu pauseMenu;

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

        if (controller.isGrounded)
        {
            move_y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                move_y = jump_speed;
            }
        }
        if (controller.transform.position.y <= -30)
        {
            controller.enabled = false;
            transform.position = new Vector3(0, 30, 0);
            controller.enabled = true;
        }

        direction = (transform.forward * move_v) + (transform.right * move_h);

        move_y -= gravity * Time.deltaTime;
        direction.y = move_y;

        controller.Move(direction * move_speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.Pause();
        }
    }
}
