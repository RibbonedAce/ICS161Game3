using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public static EnemyController instance; // The instance to reference
    public List<GameObject> enemies;        // The enemy Game Object to spawn
    public List<Vector2Int> taken;          // The spots already taken
    public GameObject SmartEnemy;
    public GameObject PatrolEnemy;
    public GameObject treasure;
    void Awake ()
    {
        instance = this;
        taken = new List<Vector2Int> { Vector2Int.zero, new Vector2Int(GameController.width - 1, 0) };
        /*for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                taken.Add(new Vector2Int(i, j));
            }
        }*/
        SpawnEnemy();
    }

    // Use this for initialization
    void Start ()
    {
        
    }
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void SpawnEnemy()
    {
        SpawnAIEnemy();
        SpawnPatrolEnemy();
        for (int i = 0; i < GameController.height * GameController.width / 10; ++i)
        {
            Vector2Int pos = GetNewVector(true);
            Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        }
        SpawnTreasure();
    }

    // Get a random value not taken
    public Vector2Int GetNewVector (bool update)
    {
        Vector2Int result = new Vector2Int(Random.Range(0, GameController.width), Random.Range(0, GameController.height));
        for (; taken.Contains(result); result = new Vector2Int(Random.Range(0, GameController.width), Random.Range(0, GameController.height))) ;
        if (update)
        {
            taken.Add(result);
        }
        return result;
    }

    // Get a random value taken not near the player or the AI
    public Vector2Int GetOldVector ()
    {
        foreach (Vector2Int v in taken)
        {
            if (v.y > GameController.height / 2)
            {
                return v;
            }
        }
        return taken[taken.Count - 1];
    }

    private void SpawnPatrolEnemy()
    {
        List<Vector2Int> SpawnLocation = new List<Vector2Int>();
        for (int i = 0; i < 2 * ((int)GameController.difficulty - 1) + GameController.height / 5; ++i)
        {
            Vector2Int pos = GetNewVector(false);
            SpawnLocation.Add(pos);
        }
        for (int i = 0; i < SpawnLocation.Count; ++i)
        {
            Instantiate(PatrolEnemy, new Vector3(SpawnLocation[i].x, SpawnLocation[i].y, 0), Quaternion.identity);
        }
    }
    private void SpawnAIEnemy()
    {
        Instantiate(SmartEnemy, new Vector3(GameController.width - 1, 0, 0), Quaternion.identity);
    }

    private void SpawnTreasure ()
    {
        Instantiate(treasure, (Vector2)GetOldVector(), Quaternion.identity);
    }
}
