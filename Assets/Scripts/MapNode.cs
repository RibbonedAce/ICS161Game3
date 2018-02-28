using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode {
    public Dictionary<Direction, MapNode> adjacents;    // The adjacent nodes
    
    // A constructor taking an index value
    public MapNode ()
    {
        adjacents = new Dictionary<Direction, MapNode>(4);
        for (int i = 0; i < 4; ++i)
        {
            adjacents.Add((Direction)i, null);
        }
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
