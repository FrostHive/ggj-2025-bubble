using System.ComponentModel;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Movement")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float runMultiplier = 1.5f;
    [SerializeField] private float jumpHeight = 200f;
    [SerializeField] private LayerMask groundLayer; // For ground detection
    [SerializeField] private float gravityScaleDuringFall = 10.0f; // Scale for gravity effect
    [SerializeField] private float jumpCooldown = 0.25f;
    [SerializeField] private int maxJumps = 2;
    private int usedJumps = 0;
    private float timeSinceJump = 0;
    private bool letGoOfJump;
    private Rigidbody rigidbody;
    private bool hasJumped;
    private bool fastFall;
    private bool facingRight = true;
    private float timeCount = 0.0f; //for rotation
    quaternion facing = new Quaternion(0,0,0,1);
    private float walkTimer = 0.3f;
    private float timer = 0;
    private bool isDead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // FixedUpdate for physics
    private void FixedUpdate()
    {
        if(!isDead)
            HandleMovement();
    }


    private void HandleMovement()
    {
        
        Vector3 currentMovement = new Vector3(inputHandler.moveInput.x, 0f, 0f) * baseSpeed;

        // Check if player is grounded
        bool isGrounded = IsGrounded();
        animator.SetBool("HasLanded", isGrounded);
        // Handle jumping
        if (inputHandler.jumpTriggered && usedJumps < maxJumps && timeSinceJump > jumpCooldown && letGoOfJump)
        {
            rigidbody.linearVelocity = Vector3.zero;
            Vector3 jumpForce = new Vector3(0f, Mathf.Sqrt(2 * jumpHeight * Physics.gravity.magnitude), 0f);
            rigidbody.AddForce(jumpForce, ForceMode.Impulse);
            usedJumps += 1;
            timeSinceJump = 0;
            letGoOfJump = false;
            timer = 0;
        }
        // Cooldown the jump
        // player has to let go of jump button to jump again
        if (!inputHandler.jumpTriggered)
        {
            letGoOfJump = true;
        }
        timeSinceJump += Time.deltaTime;

        //if falling
        if (rigidbody.linearVelocity.y <= -0.5f)
        {
            fastFall = true;
            rigidbody.AddForce(Physics.gravity * gravityScaleDuringFall - Physics.gravity, ForceMode.Acceleration);
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
        // Apply rotation
        transform.rotation = facing;
        //transform.rotation = Quaternion.Slerp(transform.rotation, facing, timeCount);
        //timeCount = timeCount + Time.deltaTime;
        if (isGrounded && currentMovement.x != 0) 
        {
            timer += Time.deltaTime;
            if (timer > walkTimer)
            {
                AudioManager.PlayOneShot(3, 5f);
                timer = 0;
            }
        }

        if (isGrounded)
        {
            // Reset jumping state if grounded and off cooldown
            if ( timeSinceJump > jumpCooldown)
            {
                usedJumps = 0;
            }
            //flip direction of character movement only when on the ground
            Facing();
        }
    }

    private void Facing()
    {
        //turn right when moving right
        if (rigidbody.linearVelocity.x > 0f && !facingRight)
        {
            timeCount = 0;
            facingRight = true;
            facing = new Quaternion(0, 0, 0, 1);
        }
        //turn left when moving left
        if (rigidbody.linearVelocity.x < 0f && facingRight)
        {
            timeCount = 0;
            facingRight = false;
            facing = new Quaternion(0, 180, 0, 1);
        }
    }

    private bool IsGrounded()
    {
        // Perform a raycast to check if the player is touching the ground
        return Physics.Raycast(transform.position, Vector3.down, 1.2f, groundLayer);
    }

    public void Dead()
    {
        isDead = true;
    }
}
