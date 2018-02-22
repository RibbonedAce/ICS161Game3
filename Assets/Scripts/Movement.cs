using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Movement : MonoBehaviour {
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        _rigidbody2D.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0));
	}

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.otherCollider.CompareTag("Enemy"))
        {

        }
    }
}
