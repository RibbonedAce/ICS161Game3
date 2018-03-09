using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AIEnemy : MonoBehaviour {

    // Use this for initialization

    public GameObject afterEffect;      // The after-effect to use
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached
    private AudioSource _audioSource;  // The Audio Source component attached

    [Range(0, 5)]
    public float _speed;

    private bool moving;
    private int start;
    private int destination;
    private List<int> visited;          // Tracks visited nodes
    private List<int> myPath;
    private int index;
    
	void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().volume = GameController.volume;
        visited = new List<int>();
        getLocations();
        //myPath = Maze.instance.FindPathOld(start, destination);
        index = 0;
        moving = false;
	}
	
	// Update is called once per frame
	void Update () {
       /* GetComponent<AudioSource>().volume = GameController.volume;
        if (index >= myPath.Count)
        {
            index = 0;
            myPath.Clear();
            getLocations();
            myPath = Maze.instance.FindPathFast(start, destination);
        }
        else
        {
            Move();
        }*/
        //if (index == myPath.Count)
        //    MenuController.instance.GameLost();
	}
    void getLocations()
    {
    }

    private void Move()
    {
        if (!moving)
        {
            StartCoroutine(MoveTo(Maze.instance.PositionAt(myPath[index]), 1 / _speed));
        }
    }
    private IEnumerator MoveTo(Vector2 destination, float time)
    {
        moving = true;
        Vector2 start = _rigidbody2D.position;
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            _rigidbody2D.MovePosition(Vector2.Lerp(start, destination, i / time));
            yield return null;
        }
        _rigidbody2D.MovePosition(destination);
        ++index;
        moving = false;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
