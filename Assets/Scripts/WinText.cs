using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class WinText : MonoBehaviour {
    private Text _text; // The Text component attached

    void Awake ()
    {
        _text = GetComponent<Text>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (GameController.status == GameStatus.Won)
        {
            _text.text = "WON";
        }
        else if (GameController.status == GameStatus.Lost)
        {
            _text.text = "LOST";
        }
        else
        {
            _text.text = "";
        }
	}
}
