using UnityEngine;
using System.Collections;

public class bossEnemy : MonoBehaviour
{
    public GameObject[] attackPoints;
    public GameObject bulletPrefab, smallEnemyPrefab, fireballPrefab;
    public float attackInterval = 1.5f;
    public int attackRepetitions = 4;
    private int currentAttackType = 0;
    private bool isTrapped = false;

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

    public void TrapBoss()
    {
        isTrapped = true;
        Invoke("ReleaseTrap", 5f);
    }

    void ReleaseTrap()
    {
        isTrapped = false;
    }
}


