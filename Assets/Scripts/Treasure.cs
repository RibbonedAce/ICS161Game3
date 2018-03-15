using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Treasure : MonoBehaviour {
    public static Treasure instance;
    public GameObject afterEffect;      // The after-effect to use
    private Rigidbody2D _rigidbody2D;   // The Rigidbody component attached
    public Vector2Int pos;
    public SpriteRenderer _spriteRenderer;
    public float colorRate;             // The rate of color change
    private bool changing = false;

    void Awake ()
    {
        instance = this;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!changing)
        {
            StartCoroutine(ChangeColor());
        }
	}

    private IEnumerator ChangeColor ()
    {
        changing = true;
        Color oldColor = _spriteRenderer.color;
        Color newColor = RandomBrightColor();
        for (float t = 0; t < colorRate; t += Time.deltaTime)
        {
            _spriteRenderer.color = Color.Lerp(oldColor, newColor, t / colorRate);
            yield return null;
        }
        _spriteRenderer.color = newColor;
        changing = false;
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            AudioSource a = Instantiate(afterEffect).GetComponent<AudioSource>();
            a.pitch = 1.5f;
            MenuController.instance.GameWon();
            Destroy(gameObject);
        }
        if (collision.collider.CompareTag("AIEnemy"))
        {
            AudioSource a = Instantiate(afterEffect).GetComponent<AudioSource>();
            a.pitch = 0.5f;
            MenuController.instance.GameLost();
            Destroy(gameObject);
        }
    }

    // Returns a random bright color
    private Color RandomBrightColor ()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float max = Mathf.Max(r, g, b);
        r = r / max;
        g = g / max;
        b = b / max;
        return new Color(r, g, b);
    }
}
