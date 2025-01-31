using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charitor : MonoBehaviour
{
    private CharacterController characterController;

    public float speed = 5.0f;
    public float sprintSpeed = 10f; // Sprinting speed
    public float normalSpeed = 5f; // Normal speed
    public float mouseSensitivity = 2.0f; // Controls how sensitive the mouse movement is
    public float jumpHeight = 2.0f; // Height of the jump
    public float gravity = -9.8f; // Gravity applied to the player
    public Transform playerBody; // The player's body, used for rotating the body around
    public Camera playerCamera; // The camera, used for rotating the view around
    private float xRotation = 0f; // Track the camera's up and down rotation

    private Vector3 velocity; // The velocity of the player
    private bool isGrounded; // Check if the player is grounded
    private bool isCeilingHit; // Check if the player is hitting the ceiling

    public float ceilingCheckDistance = 0.5f; // Distance to check for ceiling collision

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Hide and lock cursor to the center
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        // Mouse input for looking around
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // Horizontal mouse movement
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // Vertical mouse movement

        // Rotate the player body (left and right)
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate the camera up and down, with clamping to avoid flipping
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Prevent the camera from flipping over
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Get the movement input (forward/backward and left/right)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get the camera's forward and right direction, ignoring the y-axis (vertical direction)
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0f; // Flatten the forward vector to ignore vertical movement

        Vector3 right = playerCamera.transform.right;
        right.y = 0f; // Flatten the right vector to ignore vertical movement

        // Combine the input with the camera's direction to get the movement direction
        Vector3 move = (forward * vertical + right * horizontal).normalized;

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity
        }

        // Check if the player is hitting the ceiling
        isCeilingHit = Physics.Raycast(transform.position, Vector3.up, ceilingCheckDistance);

        // Apply gravity and stop upward velocity if hitting the ceiling
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime; // Apply gravity when not grounded
        }
        else
        {
            // If grounded, reset vertical velocity
            if (velocity.y < 0)
            {
                velocity.y = -2f; // Small value to keep the player grounded
            }
        }

        // Stop upward velocity if the player hits the ceiling
        if (isCeilingHit && velocity.y > 0)
        {
            velocity.y = 0f;
        }

        // Sprinting logic
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        // Move the character
        characterController.Move((move * speed + velocity) * Time.deltaTime);
    }
}
