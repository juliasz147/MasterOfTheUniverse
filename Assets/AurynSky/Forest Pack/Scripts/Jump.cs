using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 5f;
    public float jumpSpeed = 10f;
    public float gravity = -10f;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Input.GetButton("Jump"))
            {
                velocity.y = jumpSpeed;
            }
        }

        velocity.y += gravity * Time.deltaTime;
    }
}
