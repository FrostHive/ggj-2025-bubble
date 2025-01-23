using UnityEngine;

public class bullet : MonoBehaviour
{
    public float life = 3;

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        { 
        Destroy(collision.gameObject);
    }
        Destroy(gameObject);
    }
}
