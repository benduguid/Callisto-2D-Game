using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer; // Reference to the TextMeshProUGUI component

    private static float currentTime; // The current time elapsed
    private static bool stopTimer; // Flag to stop the timer

    //====================================================
    // Start is called before the first frame update
    //====================================================
    void Start()
    {
        // Initialize the current time and stopTimer flag
        if (currentTime == 0f)
            currentTime = 0f;

        if (!stopTimer)
            stopTimer = false;
    }

    //====================================================
    // Update is called once per frame
    //====================================================
    void Update()
    {
        // If the timer is not stopped, update the current time
        if (!stopTimer)
            currentTime = currentTime += Time.deltaTime;

        // If the elapsed time is less than 60 seconds, display the time with two decimal places
        if (currentTime < 60)
            timer.text = currentTime.ToString("0.00");

        else // If the elapsed time is greater than or equal to 60 seconds, display the time in hours, minutes, and seconds
        {
            int hours = Mathf.FloorToInt(currentTime / 3600f);
            int minutes = Mathf.FloorToInt((currentTime - hours * 3600f) / 60f);
            int seconds = Mathf.FloorToInt(currentTime - hours * 3600f - minutes * 60f);

            // Format the time string as "HH:MM:SS"
            string timerString = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
            timer.text = timerString;
        }
    }

    //====================================================
    // Stop the timer
    //====================================================
    public void stopTime()
    {
        stopTimer = true;
    }


    //====================================================
    // Stop the timer
    //====================================================
    public static void startTime()
    {
        stopTimer = false;
    }


    //====================================================
    // Restart the timer
    //====================================================
    public static void RestartTimer()
    {
        currentTime = 0f;
    }
}