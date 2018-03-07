using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public int size;    // The size of the maze in the level

    void Awake ()
    {
        GameController.height = size;
        GameController.width = size;
        GetComponent<AudioSource>().volume = GameController.volume;
    }

    // Use this for initialization
    void Start ()
    {
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<AudioSource>().volume = GameController.volume;
    }
}
