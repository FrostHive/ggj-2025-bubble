using UnityEngine;

public class TempWinZone : MonoBehaviour
{
    [SerializeField] private SceneChange sceneChange;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEntered was triggered");
        if (other.CompareTag("Player"))
        {
            sceneChange.LoadWinScene();
        }
    }
}
