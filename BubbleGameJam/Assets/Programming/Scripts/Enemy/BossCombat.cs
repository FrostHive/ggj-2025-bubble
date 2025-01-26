using System.Threading;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class BossCombat : MonoBehaviour
{
    [SerializeField] private bool isDead;
    public bool fightHasStarted;

    [SerializeField] private float maxAttackCooldown = 5f;
    private float currentAttackCooldown;
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
        currentAttackCooldown = .1f;
        if(rigidbody == null)
        {
            rigidbody = GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        if(isDead)
        {
            return;
        }
        if(fightHasStarted)
        {
            if (currentAttackCooldown <= 0f & IsGrounded())
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

            }
            else
            {
                currentAttackCooldown -= Time.fixedDeltaTime;
            }

            if (hasLanded != IsGrounded())
            {
                hasLanded = IsGrounded();
                if (hasLanded)
                {
                    Facing();
                }
            }
        }
    }
    private void GunkShot()
    {
        Debug.Log("Boss Bullet has been created");
        var bullet = Instantiate(filthProjectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        if (bullet != null)
        {
            bullet.GetComponent<Rigidbody>().linearVelocity = facingRight ? Vector3.left * projectileSpeed : Vector3.right * projectileSpeed;
            currentAttackCooldown = maxAttackCooldown;
            Debug.Log("Boss Bullet instantiated and velocity set");
        }
    }

    private bool IsGrounded()
    {
        // Perform a raycast to check if the player is touching the ground
        bool touchingGround = Physics.Raycast(groundDetectionPoint.position, Vector3.down, 0.5f, groundLayer);
        Debug.Log($"Touching ground: {touchingGround}");
        return touchingGround;
    }

    private void ChargerThrow()
    {
        Debug.Log("Charger has been created");
        var chargerMinion = Instantiate(chargerPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        if (chargerMinion != null)
        {
            chargerMinion.GetComponent<Rigidbody>().linearVelocity = facingRight ? Vector3.left * projectileSpeed: Vector3.right * projectileSpeed;
            currentAttackCooldown = maxAttackCooldown;
            Debug.Log("Charger instantiated and velocity set");
        }
    }

    private void Facing()
    {
        //turn right when moving right
        if (!facingRight)
        {
            facingRight = true;
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }
        //turn left when moving left
        if (facingRight)
        {
            facingRight = false;
            transform.rotation = new Quaternion(0, 180, 0, 1);
        }
    }
}
