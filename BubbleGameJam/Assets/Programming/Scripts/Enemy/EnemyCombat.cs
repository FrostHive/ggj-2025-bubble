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

    public bool kicked = false;
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
        AudioManager.PlaySound(2);
        yield return null;

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.CompareTag("Bubble"))
        {
            //Implement TrappedInBubble here
            AudioManager.PlaySound(1);
            StartCoroutine(TrappedInBubble());
            trappedInBubble = true;
        }

        if (collider.CompareTag("Player") && trappedInBubble && PlayerOnTop())
        {
            steppedOn = true;
        }
        else if (collider.CompareTag("Enemy") && kicked)
        {
            steppedOn = true;
            if (collider.GetComponent<BossCombat>())
            {
                collider.GetComponent<BossCombat>().TakeDamage(10);
                return;
            }

            collider.GetComponent<EnemyCombat>().steppedOn = true;          
        }
    }
    public IEnumerator TrappedInBubble()
    {
        //Play Bubble-trapping animation
        bubbleSphereObject.SetActive(true);
        GetComponent<EnemyLogic>().DefeatEnemy();
        yield return null;
    }
    private bool PlayerOnTop()
    {
        // Perform a raycast to check if the player is on top of an enemy
        return Physics.SphereCast(transform.position, 10f, Vector3.up, out RaycastHit hitInfo, 10f, playerLayer);
    }
}
