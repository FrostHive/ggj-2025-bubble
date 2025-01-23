using UnityEngine;
using UnityEngine.SceneManagement;
public class scenechange : MonoBehaviour
{
    // Reference to the Main Menu Canvas
    public GameObject mainMenuCanvas;
    public GameObject settingCanvas;
    public GameObject winCanvas;
    public GameObject looseCanvas;

    // Method to load a scene by its build index
    //public void Play()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +0);
    //}
    public void StartGame()
    {
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(false); // Hide the Main Menu Canvas
        }
        else
        {
            Debug.LogError("Main Menu Canvas is not assigned in the Inspector!");
        }
        looseCanvas.SetActive(false);
        // Add your gameplay start logic here, like enabling player controls
        Debug.Log("Game Started!");
    }
    public void settings()
    {
        settingCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }
    public void back()
    {
        settingCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
    public void win()
    {
        mainMenuCanvas.SetActive(true);
        winCanvas.SetActive(false);
    }
 
    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
