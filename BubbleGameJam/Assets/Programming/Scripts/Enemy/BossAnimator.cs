using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BossCombat bossCombat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        animator = GetComponent<Animator>();
        bossCombat = GetComponent<BossCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossCombat != null)
        {
            if(bossCombat.isAttacking)
            {
                animator.SetBool("Shoot", true);
            }
            else
            {
                StopAttacking();
            }

            if(bossCombat.currentJumpCooldown <= 0.5f)
            {
                animator.SetBool("Jump", true);
            }
            if(bossCombat.IsGrounded())
            {
                animator.SetBool("HasLanded", true);
                SetAllBoolsFalse();
            }
        }
    }

    public void StopAttacking()
    {
        animator.SetBool("Shoot", false);
    }

    private void SetAllBoolsFalse()
    {
        animator.SetBool("Falling", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Shoot", false);
    }
}
