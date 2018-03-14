using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController {
    public static int height = 10;                          // The height of everything
    public static int width = 10;                           // The width of everything
    public static GameStatus status = GameStatus.Playing;   // Whether the player has won/lost
    public static Difficulty difficulty = Difficulty.Hard;  // The difficulty of the game
    public static float volume = 0.5f;
    public static List<int> KillCountEnemy = new List<int>(3) { 0, 0, 0 };

    public static void ResetGameState ()
    {
        Time.timeScale = 1;
        status = GameStatus.Playing;
        KillCountEnemy = new List<int>() { 0, 0, 0 };
    }
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

public enum GameStatus
{
    Playing,
    Won,
    Lost
}