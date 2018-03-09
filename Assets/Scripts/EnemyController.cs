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
    void Awake ()
    {
        instance = this;
        ResetGameState();
        taken = new List<Vector2Int> { Vector2Int.zero };
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                taken.Add(new Vector2Int(i, j));
            }
        }
        Invoke("SpawnEnemy", 0.5f);
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
        for (int i = 0; i < GameController.height * GameController.width / 10; ++i)
        {
            Vector2Int pos = GetNewVector();
            Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        }
        SpawnPatrolEnemy();
        SpawnAIEnemy();
    }
    // Get a random value not taken
    private Vector2Int GetNewVector ()
    {
        Vector2Int result = new Vector2Int(Random.Range(0, GameController.width), Random.Range(0, GameController.height));
        for (; taken.Contains(result); result = new Vector2Int(Random.Range(0, GameController.width), Random.Range(0, GameController.height))) ;
        taken.Add(result);
        return result;
    }
    private void SpawnPatrolEnemy()
    {
        List<Vector2Int> SpawnLocation = new List<Vector2Int>();
        for (int i = 0; i < (int)(GameController.difficulty + 4); ++i)
        {
            Vector2Int pos = GetNewVector();
            SpawnLocation.Add(pos);
        }
        for (int i = 0; i < SpawnLocation.Count; ++i)
        {
            Instantiate(PatrolEnemy, new Vector3(SpawnLocation[i].x, SpawnLocation[i].y, 0), Quaternion.identity);
        }
    }
    private void SpawnAIEnemy()
    {
        Instantiate(SmartEnemy, new Vector3(GameController.width-1,0, 0), Quaternion.identity);
    }

    private void ResetGameState()
    {
        Time.timeScale = 1;
        GameController.status = GameStatus.Playing;
        GameController.KillCountEnemy = new List<int>() { 0, 0, 0 };
    }
}
