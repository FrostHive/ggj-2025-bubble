using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;

    [SerializeField] private Vector3 kickForce;
    [SerializeField] private Transform kickPoint;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float maxKickCooldown;
    private float currentCooldown;
    void Start()
    {
        currentCooldown = 0f;
    }

    void FixedUpdate()
    {
        if(inputHandler.kickTriggered && currentCooldown <= 0f)
        {
            Collider[] enemies = EnemiesInRange();
            foreach(Collider collider in enemies)
            {
                Rigidbody enemyBody = collider.GetComponent<Rigidbody>();
                EnemyCombat enemyCombat = collider.GetComponent<EnemyCombat>();
                if (enemyCombat != null && enemyCombat.trappedInBubble)
                {
                    enemyBody.AddForce(kickForce, ForceMode.Impulse);
                    enemyCombat.kicked = true;
                }
            }

            currentCooldown = maxKickCooldown;
        }
        
        if(currentCooldown >= 0f)
        {
            currentCooldown -= Time.fixedDeltaTime;
        }
    }

    private Collider[] EnemiesInRange()
    {
        // Perform a raycast to check if the player is on top of an enemy
        return Physics.OverlapSphere(kickPoint.position, 0.5f, enemyLayer);
    }
}
