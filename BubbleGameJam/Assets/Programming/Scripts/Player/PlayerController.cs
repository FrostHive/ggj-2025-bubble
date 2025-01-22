using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;

    [Header("Movement")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float runMultiplier = 1.5f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private LayerMask groundLayer; // For ground detection

    private Rigidbody rigidbody;
    private bool hasJumped;

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

        // Apply sprint multiplier if applicable
        if (inputHandler.sprintValue > 0f)
        {
            currentMovement *= runMultiplier;
        }

        // Apply movement force
        Vector3 velocity = rigidbody.linearVelocity;
        velocity.x = currentMovement.x;
        rigidbody.linearVelocity = velocity;

        // Reset jumping state if grounded
        if (isGrounded)
        {
            hasJumped = false;
        }
    }

    private bool IsGrounded()
    {
        // Perform a raycast to check if the player is touching the ground
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);
    }
}
