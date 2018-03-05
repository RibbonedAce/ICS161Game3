using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameController {
    public static int height = 10;                          // The height of everything
    public static int width = 10;                           // The width of everything
    public static bool won = false;                         // Whether the player has won
    public static Difficulty difficulty = Difficulty.Easy;  // The difficulty of the game
}

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}