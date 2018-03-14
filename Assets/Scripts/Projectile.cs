using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour {
    public float speed;                 // The speed of the projectile
    private Direction direction;        // The direction the projectile travels in
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached

    void Awake ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10);
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		switch (direction)
        {
            case Direction.Up:
                _rigidbody2D.MovePosition(transform.position + speed * new Vector3(0, Time.deltaTime, 0));
                break;
            case Direction.Down:
                _rigidbody2D.MovePosition(transform.position + speed * new Vector3(0, -Time.deltaTime, 0));
                break;
            case Direction.Left:
                _rigidbody2D.MovePosition(transform.position + speed * new Vector3(-Time.deltaTime, 0, 0));
                break;
            case Direction.Right:
                _rigidbody2D.MovePosition(transform.position + speed * new Vector3(Time.deltaTime, 0, 0));
                break;
        }
    }

    // Set the direction of the projectile
    public void SetDirection (Direction d)
    {
        direction = d;
        switch (direction)
        {
            case Direction.Up:
                transform.GetChild(0).Rotate(new Vector3(0, 0,135));
                break;
            case Direction.Down:
                transform.GetChild(0).Rotate(new Vector3(0, 0,315));
                break;
            case Direction.Left:
                transform.GetChild(0).Rotate(new Vector3(0, 0,225));
                break;
            case Direction.Right:
                transform.GetChild(0).Rotate(new Vector3(0, 0,45));
                break;
        }
        
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy") )
        {
            collision.collider.GetComponent<Enemy>().ChangeHealth(-1);
        }
        if(collision.collider.gameObject.tag == "SmartEnemy")
        {
            collision.collider.GetComponent<Enemy>().ChangeHealth(-1);
        }
        Destroy(gameObject);
    }
}
