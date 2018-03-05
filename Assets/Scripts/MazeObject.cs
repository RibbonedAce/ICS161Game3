using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeObject : MonoBehaviour {
    [Range(0f, 1f)]
    public float chance;    // The difficulty of the maze (how many walls spawn)
    public GameObject wall; // The wall prefab
    private Maze maze;      // The maze that the object uses

    void Awake ()
    {
        maze = new Maze(GameController.height, GameController.width, chance);
        SpawnWalls();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Maze GetMaze()
    {
        Maze temp = maze;
        return temp;
    }
    // Spawn walls depending on the maze
    private void SpawnWalls ()
    {
        for (int i = 0; i < GameController.width; ++i)
        {
            Instantiate(wall, new Vector3(i, -0.5f, 0), Quaternion.identity);
            Instantiate(wall, new Vector3(i, GameController.height - 0.5f, 0), Quaternion.identity);
        }
        for (int i = 0; i < GameController.height; ++i)
        {
            Instantiate(wall, new Vector3(-0.5f, i, 0), Quaternion.Euler(0, 0, 90));
            Instantiate(wall, new Vector3(GameController.width - 0.5f, i, 0), Quaternion.Euler(0, 0, 90));
        }
        foreach (MapNode mn in maze.GetNodes())
        {
            Vector2Int xy = maze.PositionAt(mn);
            if (xy.y < GameController.height - 1 && mn.adjacents[Direction.Up] == null)
            {
                Instantiate(wall, xy + new Vector2(0, 0.5f), Quaternion.identity);
            }
            if (xy.x < GameController.width - 1 && mn.adjacents[Direction.Right] == null)
            {
                Instantiate(wall, xy + new Vector2(0.5f, 0), Quaternion.Euler(0, 0, 90));
            }
        }
    }
}
