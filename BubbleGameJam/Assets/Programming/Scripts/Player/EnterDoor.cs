using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChanger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Finish"))
        {
            Debug.Log("Door found");
            sceneChanger.LoadBossScene();
        }
    }
}
