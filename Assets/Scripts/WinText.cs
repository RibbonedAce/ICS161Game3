using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (GameController.won)
        {
            GetComponent<Text>().text = "WON";
        }
        else
        {
            GetComponent<Text>().text = "";
        }
	}
}
