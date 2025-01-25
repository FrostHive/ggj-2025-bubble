using UnityEngine;
using System.Collections;

public class bossEnemy : MonoBehaviour
{
    public GameObject[] attackPoints;
    public GameObject bulletPrefab, smallEnemyPrefab, fireballPrefab;
    public GameObject bubblePrefab; // Bubble visual to show trapping
    public float attackInterval = 1.5f;
    public int attackRepetitions = 4;
    private int currentAttackType = 0;
    private bool isTrapped = false;
    private GameObject activeBubble; // Reference to the active bubble visual


    void Start()
    {
        StartCoroutine(AttackCycle());
    }

    IEnumerator AttackCycle()
    {
        while (true)
        {
            if (!isTrapped)
            {
                switch (currentAttackType)
                {
                    case 0:
                        yield return StartCoroutine(PerformAttack(bulletPrefab));
                        break;
                    case 1:
                        yield return StartCoroutine(PerformAttack(smallEnemyPrefab));
                        break;
                    case 2:
                        yield return StartCoroutine(PerformAttack(fireballPrefab));
                        break;
                }
                currentAttackType = (currentAttackType + 1) % 3; // Cycle attacks
            }
            yield return null;
        }
    }

    IEnumerator PerformAttack(GameObject attackPrefab)
    {
        for (int i = 0; i < attackRepetitions; i++)
        {
            int randomPoint = Random.Range(0, attackPoints.Length);
            Instantiate(attackPrefab, attackPoints[randomPoint].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(attackInterval);
        }
    }

 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if hit by a bubble bullet
        if (collision.CompareTag("Bubble") && !isTrapped)
        {
            TrapBoss();
            Destroy(collision.gameObject); // Destroy the bubble bullet on collision
        }
    }
    public void TrapBoss()
    {
        isTrapped = true;

        // Display bubble visual
        activeBubble = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        activeBubble.transform.SetParent(transform); // Attach bubble to boss

        // Release trap after 5 seconds
        Invoke("ReleaseTrap", 5f);
    }

    void ReleaseTrap()
    {
        isTrapped = false;
        // Destroy bubble visual
        if (activeBubble != null)
        {
            Destroy(activeBubble);
        }
    }
}


