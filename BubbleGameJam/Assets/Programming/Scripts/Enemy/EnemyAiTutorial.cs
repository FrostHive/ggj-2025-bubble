using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float detectionRange = 15f; // Range to detect the player
    public float patrolRadius = 10f; // Radius within which the enemy can patrol
    public float normalSpeed = 3.5f; // Speed during patrol
    public float chaseSpeed = 7f; // Increased speed during chase

    private NavMeshAgent agent;
    private Vector3 patrolTarget;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = normalSpeed; // Set initial speed to patrol speed
        SetNewPatrolPoint(); // Set the first patrol point
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Chase the player with increased speed
            agent.speed = chaseSpeed;
            // Chase the player if they are within detection range
            ChasePlayer();
        }
        else
        {
            // Patrol within the NavMesh at normal speed
            agent.speed = normalSpeed;
            // Patrol within the NavMesh
            Patrol();
        }
    }

    private void Patrol()
    {
        // If the agent is close to the patrol point or has no path, set a new patrol point
        if (Vector3.Distance(transform.position, patrolTarget) < 1f || !agent.hasPath)
        {
            SetNewPatrolPoint();
        }
    }

    private void SetNewPatrolPoint()
    {
        // Generate a random point within the patrol radius
        Vector3 randomPoint = transform.position + new Vector3(
            Random.Range(-patrolRadius, patrolRadius),
            0,
            Random.Range(-patrolRadius, patrolRadius)
        );

        // Ensure the point is on the NavMesh
        if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolTarget = hit.position; // Update patrol target
            agent.SetDestination(patrolTarget); // Move to the new patrol point
        }
    }

    private void ChasePlayer()
    {
        // Set the player's position as the target destination
        agent.SetDestination(player.position);
    }

    private void OnDrawGizmos()
    {
        // Visualize the patrol radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        // Visualize the detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
