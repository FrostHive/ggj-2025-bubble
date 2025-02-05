using System.Threading;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class BossCombat : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private bool isDead;
    public bool fightHasStarted;
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;

    [SerializeField] private float maxAttackCooldown = 5f;
    [SerializeField] private float attackCooldownAfterJump = 0.4f;
    [SerializeField] private float maxJumpCooldown = 10f;
    public float currentJumpCooldown = 10f;

    private float currentAttackCooldown;

    public bool isAttacking = false;

    [Header("Gunk Shot/Charger Throw Ratio")]
    [SerializeField, Range(0f, 1f)] private float gunkChargerRatio = 0.5f;

    [Header("Gunk Shot")]
    [SerializeField] private GameObject filthProjectile;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed;

    [Header("Charger Throw")]
    [SerializeField] private GameObject chargerPrefab;

    [Header("Super Jump")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundDetectionPoint;
    private bool hasLanded;

    [Header("Flipping")]
    [SerializeField] private bool facingRight = false;
    private Rigidbody rigidbody;
    private void Start()
    {
        currentHealth = maxHealth;
        currentAttackCooldown = .1f;
        isDead = false;
        if(rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        if(isDead)
        {
            sceneChanger.LoadWinScene();
            return;
        }
        if(fightHasStarted)
        {
            if (currentAttackCooldown <= 0f && IsGrounded())
            {
                float randomAttack = Random.Range(0f, 1f);
                if (randomAttack < gunkChargerRatio)
                {
                    GunkShot();
                    currentAttackCooldown = maxAttackCooldown;
                }
                else
                {
                    ChargerThrow();
                    currentAttackCooldown = maxAttackCooldown;
                }
                isAttacking = true;
            }
            else if (currentAttackCooldown >= 0f && IsGrounded())
            {
                currentAttackCooldown -= Time.fixedDeltaTime;
            }

            if (hasLanded != IsGrounded())
            {
                hasLanded = IsGrounded();
                if (hasLanded)
                {
                    Flip();
                    currentJumpCooldown = maxJumpCooldown;
                }
                else
                {
                    currentJumpCooldown = -1f;
                    currentAttackCooldown = attackCooldownAfterJump;
                    isAttacking = false;
                }
            }
            if (currentJumpCooldown >= 0f)
            {
                currentJumpCooldown -= Time.fixedDeltaTime;
            }
        }
        if (currentHealth < 0f)
        {
            isDead = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;

        if (collider.CompareTag("Bubble"))
        {
            currentHealth -= 5f; //Temporary health
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
    private void GunkShot()
    {
        //Debug.Log("Boss Bullet has been created");
        var bullet = Instantiate(filthProjectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        if (bullet != null)
        {
            bullet.GetComponent<Rigidbody>().linearVelocity = facingRight ? Vector3.left * projectileSpeed : Vector3.right * projectileSpeed;
            currentAttackCooldown = maxAttackCooldown;
           // Debug.Log("Boss Bullet instantiated and velocity set");
        }
    }
    private void ChargerThrow()
    {
        //Debug.Log("Charger has been created");
        var chargerMinion = Instantiate(chargerPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        if (chargerMinion != null)
        {
            chargerMinion.GetComponent<Rigidbody>().linearVelocity = facingRight ? Vector3.left * projectileSpeed: Vector3.right * projectileSpeed;
            currentAttackCooldown = maxAttackCooldown;
            //Debug.Log("Charger instantiated and velocity set");
        }
    }

    public bool IsGrounded()
    {
        // Perform a raycast to check if the player is touching the ground
        bool touchingGround = Physics.Raycast(groundDetectionPoint.position, Vector3.down, 0.5f, groundLayer);
        return touchingGround;
    }


    private void Flip()
    {
        //turn right when moving right
        if (!facingRight)
        {
            //Debug.Log("Now facing right");
            facingRight = true;
            transform.rotation = new Quaternion(0, 0, 0, 1);
            return;
        }
        //turn left when moving left
        if (facingRight)
        {
            //Debug.Log("Now facing left");
            facingRight = false;
            transform.rotation = new Quaternion(0, 180, 0, 1);
            return;
        }
    }
}
