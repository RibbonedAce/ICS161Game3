﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Treasure : MonoBehaviour {
    public static Treasure instance;
    public GameObject afterEffect;      // The after-effect to use
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached
    public Vector2Int pos;
    void Awake ()
    {
        instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            AudioSource a = Instantiate(afterEffect).GetComponent<AudioSource>();
            a.pitch = 1.5f;
            MenuController.instance.GameWon();
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("AIEnemy"))
        {
            AudioSource a = Instantiate(afterEffect).GetComponent<AudioSource>();
            a.pitch = 0.5f;
            MenuController.instance.GameLost();
            Destroy(gameObject);
        }
    }
}
