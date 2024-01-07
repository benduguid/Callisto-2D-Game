using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer _masterVolume; // Reference to AudioMixer
    public Toggle _fullscreenToggle; // Reference to the toggle for fullscreen

    //====================================================
    // Start is called before the first frame update
    //====================================================
    private void Start()
    {
        // Sets the Fullscreen Toggle color to green
        _fullscreenToggle.image.color = Color.green;
    }


    //====================================================
    // Sets master volume
    //====================================================
    public void SetMasterVolume(float _volume)
    {
        _masterVolume.SetFloat("Volume", _volume);
    }


    //====================================================
    // Toggles Fullscreen back and fourth
    //====================================================
    public void ToggleFullScreen()
    {
        // If the game is already in fullscreen, set it to windowed mode with a resolution of 1920x1080
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1280, 720, false);
            _fullscreenToggle.image.color = Color.red; // Indicate button colour with red
        }
        else // If the game is in windowed mode, set it to fullscreen with the 1920x1080 resoultion
        {
            Screen.SetResolution(1920, 1080, true);
            _fullscreenToggle.image.color = Color.green; // Indicate button colour with green
        }
    }
}