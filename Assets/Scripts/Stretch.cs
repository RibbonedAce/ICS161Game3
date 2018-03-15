using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Stretch : MonoBehaviour {
    private SpriteRenderer _spriteRenderer; // The Sprite Renderer Component Attached

    void Awake ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start ()
    {
        _spriteRenderer.size = new Vector2(GameController.width, GameController.height);
        transform.position = new Vector3((float)GameController.width / 2 - 0.5f, (float)GameController.height / 2 - 0.5f, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
