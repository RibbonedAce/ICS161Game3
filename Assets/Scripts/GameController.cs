using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController {
    public static int height = 10;                          // The height of everything
    public static int width = 10;                           // The width of everything
    public static GameStatus status = GameStatus.Playing;   // The status of the game
    public static Difficulty difficulty = Difficulty.Easy;  // The difficulty of the game
    public static float volume = 0.5f;
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