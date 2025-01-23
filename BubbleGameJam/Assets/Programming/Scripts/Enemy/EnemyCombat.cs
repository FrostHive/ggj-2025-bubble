using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    //Still to write
    //- Logic to trap the enemy in bubble
    //- How the enemy damages the player
    //- Navigation towards the player
    //
    //Some of these can and should be put into separate scripts

    public int health;
    public int damage;

    public bool trappedInBubble;

    private void Update()
    {
        if (health <= 0)
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
}
