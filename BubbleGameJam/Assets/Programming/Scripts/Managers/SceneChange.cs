using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    //This class is meant to become a singleton, however I haven't checked on 
    //whether or not we want to use singletons. If you find this and want to use one,
    //let me know and I'll allow it - Allen

    // Reference to the Main Menu Canvas
    public SceneField mainMenuScene;
    public SceneField level1Scene;
    public SceneField bossScene;
    public GameObject settingCanvas;
    public SceneField winScene;
    public SceneField gameOverScene;

    [SerializeField] private Animator loadScreenAnim;
    public void Start()
    {
        if (!loadScreenAnim)
            loadScreenAnim = gameObject.GetComponentInChildren<Animator>();
        loadScreenAnim.Play("FadeIn");

        PlayBGMusic();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(level1Scene);
        // Add your gameplay start logic here, like enabling player controls
        Debug.Log("Game Started!");
    }
    public void Settings()
    {
        settingCanvas.SetActive(true);
    }
    public void Back()
    {
        settingCanvas.SetActive(false);
    }

    public void LoadBossScene()
    {
        //SceneManager.LoadSceneAsync(bossScene);
        StartCoroutine(LoadSceneAsync(bossScene));
    }

    public void LoadMainMenu()
    {
        //SceneManager.LoadScene(mainMenuScene);
        StartCoroutine(LoadSceneAsync(mainMenuScene));
    }
    public void LoadWinScene()
    {
        //SceneManager.LoadScene(winScene);
        StartCoroutine(LoadSceneAsync(winScene));
    }
 
    public IEnumerator LoadGameOverScene(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(gameOverScene);
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        //Fade in black screen
        loadScreenAnim.Play("FadeOut");
        yield return new WaitForSecondsRealtime(.75f);

        // Start loading the scene asynchronously and store the operation
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        loadScreenAnim.Play("FadeIn");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

    void PlayBGMusic()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
 
        switch (currentScene)
        { 
            case 0: //menu scene
                AudioManager.PlayBgMusic(0);
                break;
            case 1://level 1 scene
                AudioManager.PlayOneShot(5,1f);
                AudioManager.PlayBgMusic(1);
                break;
            case 2:// boss scene
                AudioManager.PlayOneShot(6,1f);
                AudioManager.PlayBgMusic(2);
                break;
            case 3: // win scene
                break;
            case 4:// game over
                break;
        }
    }
}
