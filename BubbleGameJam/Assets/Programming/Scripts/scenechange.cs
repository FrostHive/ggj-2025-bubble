using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    // Reference to the Main Menu Canvas
    public GameObject mainMenuCanvas;
    public GameObject settingCanvas;
    public GameObject winCanvas;
    public GameObject loseCanvas;

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
        loseCanvas.SetActive(false);
        // Add your gameplay start logic here, like enabling player controls
        Debug.Log("Game Started!");
    }
    public void Settings()
    {
        settingCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }
    public void Back()
    {
        settingCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }
    public void Win()
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
