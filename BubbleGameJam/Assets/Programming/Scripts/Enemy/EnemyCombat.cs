using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    //Still to write
    //- Logic to trap the enemy in bubble
    //- Navigation towards the player
    //
    //Some of these can and should be put into separate scripts

    private bool trappedInBubble;
    public bool steppedOn;

    private void Start()
    {
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
            //Implement TrappedInBubble here when bubble mechanic is created
            trappedInBubble = true;
        }
    }
    public IEnumerator TrappedInBubble()
    {
        //Play Bubble-trapping animation
        yield return null;
    }

}
