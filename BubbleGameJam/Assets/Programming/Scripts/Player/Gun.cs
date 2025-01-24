using UnityEngine;

public class Gun : MonoBehaviour
{
    public PlayerInputHandler inputHandler;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;

    public float fireCooldown = 1f;
    private float currentCooldownTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.attackTriggered && currentCooldownTime <= 0f)
        {
            Debug.Log("Bullet has been created");
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            if (bullet != null)
            {
                bullet.GetComponent<Rigidbody>().linearVelocity = bulletSpawnPoint.right * bulletSpeed;
                currentCooldownTime = fireCooldown;
                Debug.Log("Bullet instantiated and velocity set");
            }
        }
        
        if (currentCooldownTime > 0f)
        {
            currentCooldownTime -= Time.deltaTime;
        }
        
    }
}
