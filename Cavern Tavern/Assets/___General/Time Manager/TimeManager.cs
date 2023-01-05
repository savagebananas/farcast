using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static bool isPaused;

    private void Awake()
    {
        isPaused = false;
    }

    public static void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        isPaused = true;
    }

    public static void UnpauseGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        isPaused = false;
    }
}
