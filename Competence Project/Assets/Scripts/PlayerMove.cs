using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float turnSmoothing = 15f;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public float speed = 6.0F;
    public float gravity = 20.0F;
    public float jumpSpeed = 8.0F;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * 75f * Time.deltaTime);
        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        if (Input.GetButton("Jump"))
            moveDirection.y = jumpSpeed;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
/*
if (controller.isGrounded)
{
    if (!Input.anyKey)
        return;
    if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * 75f * Time.deltaTime);
    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    moveDirection = transform.TransformDirection(moveDirection);
    moveDirection *= speed;

}
moveDirection.y -= gravity * Time.deltaTime;
controller.Move(moveDirection * Time.deltaTime);
}*/
