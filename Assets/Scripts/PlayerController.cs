using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class PlayerController : MonoBehaviour {
    public Camera cam;                      // The camera to use for reference
    public KeyCode hold;                    // The key to hold down while using
    public int maxHealth;                   // The maximum health of the player
    public int health;                      // The current health of the playe
    public GameObject projectile;           // The projectile to use for firing
    private bool invincible;                // Whether the player is invincible
    private Coroutine flashRoutine;         // The flashing coroutine to use
    private SpriteRenderer _spriteRenderer; // The Sprite Renderer component attached
    private Rigidbody2D _rigidbody2D;       // The Rigidbody component attached

    void Awake ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        health = maxHealth;
        invincible = false;
        flashRoutine = null;
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        _rigidbody2D.MovePosition(transform.position + new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0) * Time.deltaTime);
        if (hold == KeyCode.None || Input.GetKey(hold))
        {
            Vector3 mouseAt = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0) - transform.position;
            _rigidbody2D.MoveRotation(Vector2.SignedAngle(Vector3.right, mouseAt));
        }
        if (Input.GetAxisRaw("HFire") < 0)
        {
            Projectile p = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
            p.SetDirection(Direction.Left);
        }
        else if (Input.GetAxisRaw("HFire") > 0)
        {
            Projectile p = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
            p.SetDirection(Direction.Right);
        }
        else if (Input.GetAxisRaw("VFire") < 0)
        {
            Projectile p = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
            p.SetDirection(Direction.Down);
        }
        else if (Input.GetAxisRaw("VFire") > 0)
        {
            Projectile p = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
            p.SetDirection(Direction.Up);
        }
    }

    // Change the health of the player
    private void ChangeHealth (int change)
    {
        health = Mathf.Min(Mathf.Max(0, health + change), maxHealth);
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
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided");
            ChangeHealth(-1);
            invincible = true;
            flashRoutine = StartCoroutine(Flash());
            Invoke("RemoveInvincibility", 2);
        }
    }
}
