using UnityEngine;
using UnityEngine.Rendering;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletprefab;
    public float bulletSpeed=20;
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(" enters ");
            var bullet = Instantiate(bulletprefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
          //  bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed *Time.deltaTime;
            if (bullet != null)
            {
                bullet.GetComponent<Rigidbody>().linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
                Debug.Log("Bullet instantiated and velocity set");
            }

        }
        

        
    }
}
