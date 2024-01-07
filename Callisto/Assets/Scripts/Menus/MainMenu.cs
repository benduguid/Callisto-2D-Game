using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Time.timeScale = 1.0f; // Unpause time
        Timer.RestartTimer(); // Reset Timer
        Timer.startTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the first level
    }

    // Unpause time and restart level
    public void RestartLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Send to main menu
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    // Closes application
    public void QuitGame()
    {
        Application.Quit();
    }
}
