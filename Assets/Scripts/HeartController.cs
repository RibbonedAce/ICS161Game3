using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartController : MonoBehaviour {
    public static HeartController instance; // The instance to reference
    public PlayerController player;         // The player to get the health from
    public Transform canvas;                // The canvas to place the hearts on
    public GameObject heart;                // The heart to spawn
    private List<GameObject> hearts;        // The hearts on screen

    void Awake ()
    {
        instance = this;
        hearts = new List<GameObject>();
    }

    // Use this for initialization
    void Start ()
    {
        AddHearts(player.health);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Add a certain amount of hearts
    public void AddHearts (int amount)
    {
        int currentSize = hearts.Count;
        for (int i = currentSize; i < currentSize + amount; ++i)
        {
            GameObject g = Instantiate(heart, canvas);
            hearts.Add(g);
            g.GetComponent<CanvasSpacing>().ChangeAnchors(new Vector2(0.05f * i + 0.01f, 0.96f), new Vector2(0.05f * i + 0.04f, 1));
        }
    }

    // Delete a certain amount of herats
    public void DeleteHearts (int amount)
    {
        for (int i = 0; i < amount && hearts.Count > 0; ++i)
        {
            Destroy(hearts[hearts.Count - 1]);
            hearts.RemoveAt(hearts.Count - 1);
        }
    }
}
