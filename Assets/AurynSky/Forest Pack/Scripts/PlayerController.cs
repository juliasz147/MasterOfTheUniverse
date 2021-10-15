using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 5f;
    public float jumpSpeed = -2f;
    public float gravity = -20f;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
    }

    void movePlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (characterController.isGrounded && Input.GetButton("Jump"))
            {
                velocity.y = jumpSpeed;
            }
        }

        velocity.y += gravity * Time.deltaTime;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //start
        Vector3 move = new Vector3(x, 0, z);
        move.Normalize();

        transform.Translate(move * 150 * Time.deltaTime, Space.World);

        if(move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 15 * Time.deltaTime);


        }
        //end

        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }
}
