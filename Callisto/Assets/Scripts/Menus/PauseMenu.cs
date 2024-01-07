using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu; // Reference to pause menu game object

    //====================================================
    // Set time back to normal and exit pause menu
    //====================================================
    public void Resume()
    {
        Time.timeScale = 1.0f;
        _pauseMenu.SetActive(false);
    }


    //====================================================
    // Changes scene to main menu
    //====================================================
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(1);
    }


    //====================================================
    // Update is called once per frame
    //====================================================
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
    }
}
