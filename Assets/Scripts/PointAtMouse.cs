using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PointAtMouse : MonoBehaviour {
    private static Camera cam;          // The camera to use for reference
    public KeyCode hold;                // The key to hold down while using
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached

    private void Awake()
    {
        if (cam == null)
        {
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (hold == KeyCode.None || Input.GetKey(hold))
        {
            Vector3 mouseAt = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
            _rigidbody2D.MoveRotation(Vector2.SignedAngle(Vector3.right, mouseAt));
        }
	}
}
