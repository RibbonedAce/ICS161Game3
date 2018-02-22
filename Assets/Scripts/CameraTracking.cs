using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour {
    public Transform toTrack;   // The object to track
    public Vector3 offset;      // An offset to the tracking

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = toTrack.position + offset;
	}
}
