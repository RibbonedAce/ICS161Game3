using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stretch : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.localScale = new Vector3((float)GameController.width / 8, (float)GameController.height / 8, 1);
        transform.position = new Vector3((float)GameController.width / 2 - 0.5f, (float)GameController.height / 2 - 0.5f, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
