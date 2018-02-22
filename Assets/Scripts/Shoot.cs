using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Fire1"))
        {
            //Debug.DrawRay(transform.position, transform.right * 10, Color.red, 1);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 10);
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<Enemy>().ChangeHealth(-1);
            }
        }
	}
}
