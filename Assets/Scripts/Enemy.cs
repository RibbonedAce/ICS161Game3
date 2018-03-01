using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour {
    public int maxHealth;               // The maximum health of the enemy
    public int health;                  // The health of the enemy
    //public int range;                   // The range to be randomly placed
    public GameObject afterEffect;      // The after-effect to use
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached
    private AudioSource _audioSource;   // The Audio Source component attached

    void Awake ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
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
        _audioSource.pitch = 2 - (float)health / maxHealth;
        _audioSource.Play();
    }

    // Called when the enemy dies
    public void Die ()
    {
        Instantiate(afterEffect);
        Destroy(gameObject);
    }
}
