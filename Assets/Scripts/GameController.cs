using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController {
    public static int height = 10;                          // The height of everything
    public static int width = 10;                           // The width of everything
    public static GameStatus status = GameStatus.Playing;   // Whether the player has won/lost
    public static Difficulty difficulty = Difficulty.Easy;  // The difficulty of the game
    public static float volume = 0.5f;
    public static int KillCountEnemy1 = 0;
    public static int KillCountEnemy2 = 0;
    public static int KillCountEnemy3 = 0;
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