using UnityEngine;

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

    private void Start()
    {
        currentAttackCooldown = .1f;
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
                if (randomAttack< gunkChargerRatio)
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
        }
    }
    private void GunkShot()
    {
        Debug.Log("Boss Bullet has been created");
        var bullet = Instantiate(filthProjectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        if (bullet != null)
        {
            bullet.GetComponent<Rigidbody>().linearVelocity = Vector3.left * projectileSpeed;
            currentAttackCooldown = maxAttackCooldown;
            Debug.Log("Boss Bullet instantiated and velocity set");
        }
    }

    private bool IsGrounded()
    {
        // Perform a raycast to check if the player is touching the ground
        bool touchingGround = Physics.Raycast(groundDetectionPoint.position, Vector3.down, 0.1f, groundLayer);
        Debug.Log($"Touching ground: {touchingGround}");
        return touchingGround;
    }

    private void ChargerThrow()
    {
        Debug.Log("Charger has been created");
        var chargerMinion = Instantiate(chargerPrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        if (chargerMinion != null)
        {
            chargerMinion.GetComponent<Rigidbody>().linearVelocity = Vector3.left * projectileSpeed;
            currentAttackCooldown = maxAttackCooldown;
            Debug.Log("Charger instantiated and velocity set");
        }
    }
}
