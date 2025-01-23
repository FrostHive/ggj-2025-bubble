using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int currentHealth;
    [SerializeField] private const int MAXHEALTH = 10;

    private void Start()
    {
        currentHealth = MAXHEALTH;
    }
    private void Update()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(Death());
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
}
