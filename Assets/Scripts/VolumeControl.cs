using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeControl : MonoBehaviour {

    void Awake ()
    {
        ChangeVolume();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Change the volume
    public void ChangeVolume ()
    {
        GetComponent<AudioSource>().volume = GameController.volume;
    }
}
