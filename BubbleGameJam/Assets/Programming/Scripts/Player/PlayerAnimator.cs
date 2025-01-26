using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private Animator animator;
    public bool disableMovement;
    public bool disableIdle;

    private void Start()
    {
        animator = GetComponent<Animator>();
        disableMovement = false;
    }

    private void Update()
    {
        if (animator != null && !disableMovement)
        {
            SetAllBoolsFalse();
            if (inputHandler.attackTriggered)
            {
                animator.SetBool("Shoot", true);
                disableMovement = true;
                disableIdle = true;
            }

            else if ((inputHandler.moveInput.x <= -0.1f || inputHandler.moveInput.x >= 0.1f) && !disableMovement)
            {
                animator.SetBool("Run", true);
                disableIdle = true;
            }
            else
            {
                disableIdle = false;
            }

            if (inputHandler.jumpTriggered)
            {
                animator.SetBool("Jump", true);
                disableMovement = true;
                disableIdle = true;
            }

            if (inputHandler.kickTriggered)
            {
                animator.SetBool("Kick", true);
                disableIdle = true;
                disableMovement = true;
            }
        }
        else if (animator == null)
        {
            Debug.LogError("Animator is not attatched to player!");
        }
    }

    private void SetAllBoolsFalse()
    {
        animator.SetBool("Jump", false);
        animator.SetBool("Run", false);
        animator.SetBool("Kick", false);
        animator.SetBool("Shoot", false);
    }

    public void EnableMovement()
    {
        SetAllBoolsFalse();
        disableMovement = false;
        disableIdle = false;
    }
}
