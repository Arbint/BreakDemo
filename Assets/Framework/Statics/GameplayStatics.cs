using UnityEngine;

public static class GameplayStatics 
{
    public static void SetGamePaused(bool bPaused)
    {
        Time.timeScale = bPaused ? 0 : 1;
    }
}
