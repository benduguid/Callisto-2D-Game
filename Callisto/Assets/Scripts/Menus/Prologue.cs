using UnityEngine;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour
{

    //====================================================
    // Update is called once per frame
    //====================================================
    void Update()
    {
        // Exit scene and move onto main menu if any of the correct keys are pressed
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
