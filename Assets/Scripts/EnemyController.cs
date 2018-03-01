using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public static List<GameObject> enemies; // The enemy Game Object to spawn
    public static List<Vector2Int> taken;   // The spots already taken

    void Awake ()
    {
        if (enemies == null)
        {
            enemies = new List<GameObject>();
            DirectoryInfo d = new DirectoryInfo("Assets/Resources/Prefabs");
            FileInfo[] files = d.GetFiles("Enemy*.prefab");
            foreach (FileInfo f in files)
            {
                enemies.Add(Resources.Load<GameObject>("Prefabs/" + f.Name.Substring(0, f.Name.Length - 7)));
            }
        }
        if (taken == null)
        {
            taken = new List<Vector2Int> { Vector2Int.zero };
        }
    }

    // Use this for initialization
    void Start ()
    {
		for (int i = 0; i < GameController.height * GameController.width / 10; ++i)
        {
            Vector2Int pos = GetNewVector();
            Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(pos.x, pos.y, 0), Quaternion.identity);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Get a random value not taken
    private Vector2Int GetNewVector ()
    {
        Vector2Int result = new Vector2Int(Random.Range(0, GameController.width), Random.Range(0, GameController.height));
        for (; taken.Contains(result); result = new Vector2Int(Random.Range(0, GameController.width), Random.Range(0, GameController.height))) ;
        taken.Add(result);
        return result;
    }
}
