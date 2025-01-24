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
    public GameObject settingCanvas;
    public SceneField winScene;
    public SceneField gameOverScene;

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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
    public void LoadWinScene()
    {
        SceneManager.LoadScene(winScene);
    }
 
    public IEnumerator LoadGameOverScene(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(gameOverScene);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
