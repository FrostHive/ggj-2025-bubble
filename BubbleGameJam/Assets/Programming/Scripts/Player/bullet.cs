using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("LockedDoor"))
        {
            Debug.Log("Door Unlocked");
            //TODO change this to animation on door
            collider.tag = "Finish";
        }
    }
}
