using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChange;
    [SerializeField] private bool isDead = false;

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
        if (currentHealth <= 0 && !isDead)
        {
            StartCoroutine(Death(1));
            isDead = true;
            GetComponent<PlayerController>().Dead();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;

        if (collider.CompareTag("Enemy"))
        {
            if (collider.TryGetComponent<EnemyCombat>(out var enemyCombat))
            {
                if(enemyCombat.trappedInBubble && IsOnEnemy())
                {
                    Rigidbody rb = GetComponent<Rigidbody>();
                    Vector3 jumpForce = new Vector3(0f, enemyStompBoost, 0f);
                    rb.AddForce(jumpForce, ForceMode.Impulse);
                }

                else if (!enemyCombat.trappedInBubble)
                {
                    StartCoroutine(TakeDamage(enemyCombat.damage));
                }
            }
            else
            {
                Debug.LogError("Error: The enemy collider does not have the EnemyCombat component enabled or assigned");
            }
        }
        if(collider.CompareTag("DeathTrap"))
        {
            StartCoroutine(TakeDamage(999));
        }
    }
    private IEnumerator Death(int seconds)
    {
        //**Play death animation here
        AudioManager.PlaySound(4);
        StartCoroutine(sceneChange.LoadGameOverScene(seconds));

        yield return new WaitForEndOfFrame();

        //gameObject.SetActive(false);
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
