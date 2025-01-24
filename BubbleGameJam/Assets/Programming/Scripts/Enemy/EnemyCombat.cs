using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    //Still to write
    //- Logic to trap the enemy in bubble
    //- Navigation towards the player
    //
    //Some of these can and should be put into separate scripts

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
        if (collider.tag == "Bubble")
        {
            //Implement TrappedInBubble here
            bubbleSphereObject.SetActive(true);
            trappedInBubble = true;
        }

        if (collider.tag == "Player" && trappedInBubble && PlayerOnTop())
        {
            steppedOn = true;
        }
    }
    public IEnumerator TrappedInBubble()
    {
        //Play Bubble-trapping animation
        yield return null;
    }
    private bool PlayerOnTop()
    {
        // Perform a raycast to check if the player is on top of an enemy
        return Physics.SphereCast(transform.position, 0.5f, Vector3.up, out RaycastHit hitInfo, 0.5f, playerLayer);
    }
}
