using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AIEnemy : MonoBehaviour {

    // Use this for initialization

    public GameObject afterEffect;      // The after-effect to use
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached
    private AudioSource _audioSource;  // The Audio Source component attached

    [Range(0, 50)]
    public float _speed;

    private int start;
    private int destination;
    private List<int> myPath;
    private int index;
    
	void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().volume = GameController.volume;

        getLocations();
        myPath = Maze.FindPathOld(start, destination);
        index = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        //if (index == myPath.Count)
        //    MenuController.instance.GameLost();
	}
    void getLocations()
    {
        foreach (MapNode mn in Maze.nodes)
        {
            Vector2Int temp = PositionAt(mn);
            if(temp.x == (int)transform.position.x && temp.y == (int)transform.position.y)
            {
                start = Maze.nodes.IndexOf(mn);
            }
            if (temp.x == Treasure.instance.pos.x && temp.y == Treasure.instance.pos.y)
            {
                destination = Maze.nodes.IndexOf(mn);
            }
        }
    }
    private Vector2Int PositionAt(MapNode mn)
    {
        return new Vector2Int(Maze.nodes.IndexOf(mn) % Maze._width, Maze.nodes.IndexOf(mn) / Maze._width);
    }

    private Vector2Int PositionAtIndex(int index)
    {
        return new Vector2Int(index % Maze._width, index / Maze._width);
    }
    private void Move()
    {
        if(index < myPath.Count)
        {
            Vector3 res = new Vector3(PositionAtIndex(myPath[index]).x, PositionAtIndex(myPath[index]).y, 0);
            Vector3 offset = res - transform.position;
            _rigidbody2D.MovePosition(transform.position + offset * (_speed + (2.5f * (float)(GameController.difficulty)) * Time.deltaTime));
            if (transform.position == res)
                 ++index;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
