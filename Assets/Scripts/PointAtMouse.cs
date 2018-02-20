using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtMouse : MonoBehaviour {
    private static Camera cam;  // The camera to use for reference

    private void Awake()
    {
        if (cam == null)
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 relativeCam = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
        Vector3 mouseAt = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        transform.rotation = Quaternion.LookRotation(mouseAt - relativeCam, Vector3.back);
	}
}
