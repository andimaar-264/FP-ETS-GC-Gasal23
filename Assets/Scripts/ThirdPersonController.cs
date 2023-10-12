using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public float jumpHeight = 2.0f; // Jump height
    public float gravity = -9.81f;  // Gravity force
    public float groundDrag = 6f;    // Ground drag to slow down when grounded
    public float airDrag = 2f;       // Air drag when jumping

    private CharacterController controller;
    public ThirdPersonMovement thirdPersonMovement;
    private Vector3 velocity;
    private bool isOnPlatform;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isOnPlatform = Physics.CheckSphere(transform.position, 0.2f);

        if (isOnPlatform && velocity.y < 0)
        {
            velocity.y = -6.0f; // Apply a small force to keep the character grounded
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;

        if (isOnPlatform)
        {
            controller.Move(move * Time.deltaTime * thirdPersonMovement.speed * groundDrag);
        }
        else
        {
            controller.Move(move * Time.deltaTime * thirdPersonMovement.speed * airDrag);
        }

        // Jumping
        if (isOnPlatform && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
