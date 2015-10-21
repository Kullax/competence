using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float turnSmoothing = 15f;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    public float speed = 6.0F;
    public float gravity = 20.0F;
 
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

	// Update is called once per frame
	void Update () {

/*        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal"), 0) * 75f * Time.deltaTime);*/
        moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical")); 
        if (Input.GetKey(KeyCode.A))
            moveDirection += new Vector3(-1, 0, 0);
        else if (Input.GetKey(KeyCode.D))
            moveDirection += new Vector3(1, 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        controller.Move(moveDirection * Time.deltaTime);
    }
}