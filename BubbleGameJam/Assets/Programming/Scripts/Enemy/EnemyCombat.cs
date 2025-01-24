using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    //Still to write
    //- Navigation towards the player
    //
    //Some of these can and should be put into separate scripts

    public int damage;

    [Header("Bubble Trap")]
    [SerializeField] private GameObject bubbleSphereObject;
    [SerializeField] private LayerMask playerLayer;

    public bool trappedInBubble;
    private bool steppedOn;


    private void Start()
    {
        bubbleSphereObject.SetActive(false);
        trappedInBubble = false;
        steppedOn = false;
    }

    private void Update()
    {
        if (steppedOn)
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

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.CompareTag("Bubble"))
        {
            //Implement TrappedInBubble here
            StartCoroutine(TrappedInBubble());
            trappedInBubble = true;
        }

        if (collider.CompareTag("Player") && trappedInBubble && PlayerOnTop())
        {
            steppedOn = true;
        }
    }
    public IEnumerator TrappedInBubble()
    {
        //Play Bubble-trapping animation
        bubbleSphereObject.SetActive(true);
        yield return null;
    }
    private bool PlayerOnTop()
    {
        // Perform a raycast to check if the player is on top of an enemy
        return Physics.SphereCast(transform.position, 0.5f, Vector3.up, out RaycastHit hitInfo, 0.5f, playerLayer);
    }
}
