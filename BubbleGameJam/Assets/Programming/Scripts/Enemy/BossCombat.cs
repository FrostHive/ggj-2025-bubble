using UnityEngine;

public class BossCombat : MonoBehaviour
{
    [SerializeField] private bool isDead;
    [SerializeField] private float maxAttackCooldown = 5f;
    private float currentAttackCooldown;

    [Header("Gunk Shot")]
    [SerializeField] private GameObject filthProjectile;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private float projectileSpeed;

    [Header("Charger Throw")]
    [SerializeField] private GameObject chargerPrefab;


    private void FixedUpdate()
    {
        if(isDead)
        {
            return;
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

    private void Jump()
    {

    }

    private void ChargerThrow()
    {

    }
}
