using System;
using System.ComponentModel;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;

    [Header("Movement")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float runMultiplier = 1.5f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private LayerMask groundLayer; // For ground detection
    [SerializeField] private float gravityScaleDuringFall = 20.0f; // Scale for gravity effect

    private Rigidbody rigidbody;
    private bool hasJumped;
    private bool fastFall;
    private bool facingRight = true;
    private float timeCount = 0.0f; //for rotation
    quaternion facing = new Quaternion(0,0,0,1);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // FixedUpdate for physics
    private void FixedUpdate()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        
        Vector3 currentMovement = new Vector3(inputHandler.moveInput.x, 0f, 0f) * baseSpeed;

        // Check if player is grounded
        bool isGrounded = IsGrounded();

        // Handle jumping
        if (inputHandler.jumpTriggered && !hasJumped && isGrounded)
        {
            Vector3 jumpForce = new Vector3(0f, Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude), 0f);
            rigidbody.AddForce(jumpForce, ForceMode.Impulse);
            hasJumped = true;
        }

        if (!fastFall)
        {
            //if falling
            if (rigidbody.linearVelocity.y <= -0.5f)
            {
                fastFall = true;
                rigidbody.AddForce(Physics.gravity * gravityScaleDuringFall - Physics.gravity, ForceMode.Acceleration);
                Debug.Log("fastFall");
            }
        }

        // Apply sprint multiplier if applicable
        if (inputHandler.sprintValue > 0f)
        {
            currentMovement *= runMultiplier;
        }

        // Apply movement force
        Vector3 velocity = rigidbody.linearVelocity;
        velocity.x = currentMovement.x;
        rigidbody.linearVelocity = velocity;

        
        if (isGrounded)
        {
            // Reset jumping state if grounded
            hasJumped = false;
            //undo bonus gravity from fastfall
            if (fastFall == true)
            {
                fastFall = false;
                rigidbody.AddForce(-Physics.gravity * gravityScaleDuringFall - Physics.gravity, ForceMode.Acceleration);
            }
            //flip direction of character movement only when on the ground
            Facing();
        }
    }

    private void Facing()
    {
        //turn right
        if (rigidbody.linearVelocity.x > 0f && !facingRight)
        {
            timeCount = 0;
            facingRight = true;
            facing = new Quaternion(0, 0, 0, 1);
        }
        if (rigidbody.linearVelocity.x < 0f && facingRight)
        {
            timeCount = 0;
            facingRight = false;
            facing = new Quaternion(0, 180, 0, 1);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, facing, timeCount);
        timeCount = timeCount + Time.deltaTime;
    }

    private bool IsGrounded()
    {
        // Perform a raycast to check if the player is touching the ground
        return Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);
    }
}
