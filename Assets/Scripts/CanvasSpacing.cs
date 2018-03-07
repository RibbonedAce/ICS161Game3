using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSpacing : MonoBehaviour {
    private RectTransform _rectTransform;   // The Rect Transform component attached

    void Awake ()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Change the anchor positions and zero out the rest
    public void ChangeAnchors (Vector2 min, Vector2 max)
    {
        _rectTransform.anchorMax = max;
        _rectTransform.anchorMin = min;
        _rectTransform.anchoredPosition = Vector2.zero;
        _rectTransform.sizeDelta = Vector2.zero;
    }
}
