using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public static GameObject enemy; // The enemy Game Object to spawn
    public static List<Vector2Int> taken;   // The spots already taken

    void Awake ()
    {
        if (enemy == null)
        {
            enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        }
        if (taken == null)
        {
            taken = new List<Vector2Int>();
        }
    }

    // Use this for initialization
    void Start ()
    {
		for (int i = 0; i < GameController.height * GameController.width / 10; ++i)
        {
            Vector2Int pos = GetNewVector();
            Instantiate(enemy, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
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
