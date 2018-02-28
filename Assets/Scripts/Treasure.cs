using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Treasure : MonoBehaviour {
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached

    void Awake ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {
        Vector2Int toGo = new Vector2Int(GameController.width / 2, GameController.height - 1);
        _rigidbody2D.MovePosition(toGo);
        EnemyController.taken.Add(toGo);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            GameController.won = true;
            Destroy(gameObject);
        }
    }
}
