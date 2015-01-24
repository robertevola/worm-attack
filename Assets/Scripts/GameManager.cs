using UnityEngine;
using System.Collections;

public static class GameManager  
{
    public static bool isPaused = false;
    public static int currentScore = 0;

    public static void TogglePause()
    {
        if(isPaused)
        {
            Time.timeScale = 1.0f;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0.0f;
            isPaused = true;
        }
    }

    public static void IncreaseScore(int value)
    {
        currentScore += value;
        Scorebar.SetScoreText(value);
    }
}
