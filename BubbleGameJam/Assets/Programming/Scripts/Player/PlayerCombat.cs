using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private int maxHealth = 10;

    [SerializeField] private LayerMask enemyLayer; // For enemy stomp detection
    [SerializeField] private float enemyStompBoost = 5f; //Boosts the player whenever they step on a bubbled enemy


    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;

        if (collider.tag == "Enemy")
        {
            EnemyCombat enemyCombat = collider.GetComponent<EnemyCombat>();
            if (enemyCombat != null)
            {
                if(enemyCombat.trappedInBubble && IsOnEnemy())
                {
                    Rigidbody rb = GetComponent<Rigidbody>();
                    Vector3 jumpForce = new Vector3(0f, enemyStompBoost, 0f);
                    rb.AddForce(jumpForce, ForceMode.Impulse);
                }
            }
        }
    }
    private IEnumerator Death()
    {
        //**Play death animation here
        yield return null;

        Destroy(gameObject);
    }
    public IEnumerator TakeDamage(int damageAmount)
    {
        //Play damage animation
        yield return null;
        currentHealth -= damageAmount;
    }

    private bool IsOnEnemy()
    {
        // Perform a raycast to check if the player is touching the top of an enemy
        return Physics.Raycast(transform.position, Vector3.down, 1f, enemyLayer);
    }
}
