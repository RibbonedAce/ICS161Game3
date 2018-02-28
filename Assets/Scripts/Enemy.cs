using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour {
    public int maxHealth;               // The maximum health of the enemy
    public int health;                  // The health of the enemy
    public int range;                   // The range to be randomly placed
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached

    void Awake ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        health = maxHealth;   
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Changes health of the enemy
    public void ChangeHealth (int change)
    {
        health = Mathf.Min(Mathf.Max(0, health + change), maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    // Called when the enemy dies
    public void Die ()
    {
        Destroy(gameObject);
    }
}
