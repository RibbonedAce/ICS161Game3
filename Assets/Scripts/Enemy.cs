using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour {
    public int maxHealth;               // The maximum health of the enemy
    public int health;                  // The health of the enemy
    public int damage;                  // The damage the enemy deals
    public int indexName;
    //public int range;                   // The range to be randomly placed
    public GameObject afterEffect;      // The after-effect to use
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached
    private AudioSource _audioSource;  // The Audio Source component attached

    //Added vars for Smart Enemy------------
    [Range(0.1f,5)]
    public float _speed;
    private bool moving;
    private List<int> myPath;
    private List<int> notTaken;
    private List<MapNode> myNodes;
    private List<Vector2Int> Taken;
    private int index;
    private bool reverse;
    //--------------------------------------

    void Awake ()
    {
        moving = false;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().volume = GameController.volume;
        health = maxHealth;
        if(this.gameObject.tag == "SmartEnemy")
        {
            index = 0;
            reverse = false;    
            Taken = EnemyController.instance.taken;
            myNodes = Maze.instance.nodes;
            notTaken = new List<int>();
            AddNotTakenNodes();
            myPath = Maze.instance.FindPathFast(notTaken[Random.Range(1, notTaken.Count - 1)],notTaken[Random.Range(1, notTaken.Count - 1)]);
            Vector3 temp = GetNodePosition(myPath[0]);
            transform.position = new Vector3(temp.x,temp.y,0);
        }
    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called for smart enemy only
	void Update()
    {
        if (this.gameObject.tag == "SmartEnemy")
        {
            if (index >= myPath.Count)
            {
                SwitchDirections();
            }
            /*if (!reverse)
                moveForward();
            else
                moveBackward();
                */
            if (!moving)
            {
                StartCoroutine(MoveTo(GetNodePosition(myPath[index]), 1f / _speed));
            }
        }
        GetComponent<AudioSource>().volume = GameController.volume;
    }

    // Changes health of the enemy
    public void ChangeHealth (int change)
    {
        health = Mathf.Min(Mathf.Max(0, health + change), maxHealth);
        if (health <= 0)
        {
            Die();
        }
        else
        {
            _audioSource.pitch = 2 - (float)health / maxHealth;
            _audioSource.Play();
        }
    }

    // Called when the enemy dies
    public void Die ()
    {
        GameController.KillCountEnemy[indexName]++;
        Instantiate(afterEffect);
        Destroy(gameObject);     
    }


    //=================================== Smart Enemy Functions Below ============================================================
 
    private void SwitchDirections()
    {
        index = 0;
        myPath.Reverse();
    }

    private IEnumerator MoveTo (Vector2 destination, float time)
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

    private Vector3 GetNodePosition(int index)
    {
        Vector3 result = new Vector3(index % Maze._width, index / Maze._width,0);
        return result;
    }
    private Vector2Int PositionAt(MapNode mn)
    {
        return new Vector2Int(myNodes.IndexOf(mn) % Maze._width, myNodes.IndexOf(mn) / Maze._width);
    }
    public void AddNotTakenNodes()
    {
        foreach(MapNode mn in myNodes)
        {
            Vector2Int temp = PositionAt(mn);
            if(!Taken.Contains(temp))
                notTaken.Add(myNodes.IndexOf(mn));
        }
    }

    /*private void moveForward()
  {
      if (index < myPath.Count)
      {
          Vector3 result = GetNodePosition(myPath[index]);
          transform.position = Vector3.Lerp(transform.position, result,_speed);
          if(transform.position == result)
              ++index;
      }
      else reverse = true;
  }
  private void moveBackward()
  {
      if (index > 0)
      {
          Vector3 result = GetNodePosition(myPath[index - 1]);
          transform.position = Vector3.Lerp(transform.position, result,_speed);
          if (transform.position == result)
              --index;
      }
      else reverse = false;
  }*/
}
