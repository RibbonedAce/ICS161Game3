using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {
    public Camera cam;                      // The camera to use for reference
    public KeyCode hold;                    // The key to hold down while using
    public float speed;                     // The speed the player moves at
    public int maxHealth;                   // The maximum health of the player
    public int health;                      // The current health of the playe
    public GameObject projectile;           // The projectile to use for firing
    private bool invincible;                // Whether the player is invincible
    private bool fired;                     // If the player has fired
    private Coroutine flashRoutine;         // The flashing coroutine to use
    private SpriteRenderer _spriteRenderer; // The Sprite Renderer component attached
    private Rigidbody2D _rigidbody2D;       // The Rigidbody component attached
    private AudioSource _audioSource;       // The Audio Source component attached

    void Awake ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        health = maxHealth;
        invincible = false;
        flashRoutine = null;
        GetComponent<AudioSource>().volume = GameController.volume;
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        _rigidbody2D.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * speed * Time.deltaTime);
        if (hold == KeyCode.None || Input.GetKey(hold))
        {
            Vector3 mouseAt = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0) - transform.position;
            _rigidbody2D.MoveRotation(Vector2.SignedAngle(Vector3.right, mouseAt));
        }
        if (Input.GetAxisRaw("HFire") < 0 && !fired)
        {
            Fire(Direction.Left);
        }
        else if (Input.GetAxisRaw("HFire") > 0 && !fired)
        {
            Fire(Direction.Right);
        }
        else if (Input.GetAxisRaw("VFire") < 0 && !fired)
        {
            Fire(Direction.Down);
        }
        else if (Input.GetAxisRaw("VFire") > 0 && !fired)
        {
            Fire(Direction.Up);
        }
        else if (Input.GetAxisRaw("HFire") == 0 && Input.GetAxisRaw("VFire") == 0)
        {
            fired = false;
        }
        GetComponent<AudioSource>().volume = GameController.volume;
    }

    // Fire a projectile in a direction
    private void Fire (Direction d)
    {
        fired = true;
        Projectile p = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        _audioSource.Play();
        p.SetDirection(d);
    }

    // Change the health of the player
    private void ChangeHealth (int change)
    {
        health = Mathf.Min(Mathf.Max(0, health + change), maxHealth);
        if (health <= 0)
        {
            GameController.status = GameStatus.Lost;
        }
        if (change < 0)
        {
            HeartController.instance.DeleteHearts(Mathf.Abs(change));
        }
        else
        {
            HeartController.instance.AddHearts(Mathf.Abs(change));
        }
    }

    // Change invincibility status to false
    private void RemoveInvincibility ()
    {
        invincible = false;
        StopCoroutine(flashRoutine);
        _spriteRenderer.color = Color.white;
    }

    // Flash indefinitely
    private IEnumerator Flash ()
    {
        while (true)
        {
            _spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(0.25f);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.25f);
        }
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if ((collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("SmartEnemy")) && !invincible)
        {
            ChangeHealth(-1 * collision.collider.GetComponent<Enemy>().damage);
            invincible = true;
            flashRoutine = StartCoroutine(Flash());
            Invoke("RemoveInvincibility", 2);
        }
    }
}
