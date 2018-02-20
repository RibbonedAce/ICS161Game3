using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public int maxHealth;   // The maximum health of the enemy
    public int health;      // The health of the enemy

    void Awake ()
    {
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
