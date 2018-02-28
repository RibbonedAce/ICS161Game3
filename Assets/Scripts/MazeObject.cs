using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeObject : MonoBehaviour {
    public int height;      // The height of the maze to make
    public int width;       // The width of the maze to make
    [Range(0f, 1f)]
    public float chance;    // The difficulty of the maze (how many walls spawn)
    public GameObject wall; // The wall prefab
    private Maze maze;      // The maze that the object uses

    void Awake ()
    {
        maze = new Maze(height, width, chance);
        SpawnWalls();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Spawn walls depending on the maze
    private void SpawnWalls ()
    {
        for (int i = 0; i < width; ++i)
        {
            if (i != 0)
            {
                Instantiate(wall, new Vector3(i, -0.5f, 0), Quaternion.identity);
            }
            if (i != width / 2)
            {
                Instantiate(wall, new Vector3(i, height - 0.5f, 0), Quaternion.identity);
            }
        }
        for (int i = 0; i < height; ++i)
        {
            Instantiate(wall, new Vector3(-0.5f, i, 0), Quaternion.Euler(0, 0, 90));
            Instantiate(wall, new Vector3(width - 0.5f, i, 0), Quaternion.Euler(0, 0, 90));
        }
        foreach (MapNode mn in maze.GetNodes())
        {
            Vector2Int xy = maze.PositionAt(mn);
            if (xy.y < height - 1 && mn.adjacents[Direction.Up] == null)
            {
                Instantiate(wall, xy + new Vector2(0, 0.5f), Quaternion.identity);
            }
            if (xy.x < width - 1 && mn.adjacents[Direction.Right] == null)
            {
                Instantiate(wall, xy + new Vector2(0.5f, 0), Quaternion.Euler(0, 0, 90));
            }
        }
    }
}
