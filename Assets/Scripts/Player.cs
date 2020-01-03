using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    private static Player player;
    private Vector3 lastCheckpoint;
    private float reloadTime;

    public GameObject ammo;

    private float health;
    private Animator animator;
    private BoxCollider2D BoxCollider2D;
    private Rigidbody2D Rigidbody;
    private bool jetpack;
    private bool walking;
    private bool dead;


    // Start is called before the first frame update
    void Awake()
    {
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);

        this.Rigidbody = GetComponent<Rigidbody2D>();
        this.BoxCollider2D = GetComponent<BoxCollider2D>();
        this.animator = GetComponent<Animator>();
        lastCheckpoint = transform.position;
    }

    void Start()
    {
        health = 100f;
        dead = false;
        jetpack = false;
        this.transform.position = lastCheckpoint;
        reloadTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            inputPC();
            checkIfDead();
        }

    }
    private void Update()
    {
        if (!dead)
        {
            triggerFire();
        }

        if (player.dead)
        {
            reloadTime += Time.deltaTime;
            if (reloadTime >=3f)
            {
                reloadTime = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Start();
                animator.SetBool("Damage", false);
                animator.SetTrigger("Resurrect");
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Jetpack_Stop");
            jetpack = false;
        }
    }

    public override void Damage(float damage)
    {
        health -= damage;
        animator.SetTrigger("Damage");
    }
    private void inputPC()
    {
        walking = false;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            this.Rigidbody.AddRelativeForce(new Vector2(0, Rigidbody.mass * 11), ForceMode2D.Force);
            animator.SetTrigger("Jetpack_Start");
            jetpack = true;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (!jetpack)
            {
                animator.SetTrigger("Walk");
                animator.SetBool("Jetpack_Start", false);
                animator.SetBool("Jetpack_Stop", false);
                walking = true;
            }
            this.Rigidbody.AddRelativeForce(new Vector2(Rigidbody.mass * 9.81f * -0.6f, 0), ForceMode2D.Force);
            this.transform.localScale = new Vector2(-0.14f, 0.14f);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!jetpack)
            {
                animator.SetTrigger("Walk");
                animator.SetBool("Jetpack_Start", false);
                animator.SetBool("Jetpack_Stop", false);
                walking = true;
            }
            this.Rigidbody.AddRelativeForce(new Vector2(Rigidbody.mass * 9.81f * 0.6f, 0), ForceMode2D.Force);
            this.transform.localScale = new Vector2(0.14f, 0.14f);

        }

        if (!walking && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            animator.SetTrigger("Stop_Walking");
    }
    private void triggerFire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!jetpack)
            {
                animator.SetTrigger("Shoot");
            }

            if (transform.localScale.x < 0)
            {
                Vector3 pos = transform.position;
                pos.x -= 0.5f;
                GameObject AmoFired = Instantiate(ammo, pos, Quaternion.identity);
                Rigidbody2D rigidbody = AmoFired.GetComponent<Rigidbody2D>();
                AmoFired.transform.localScale = new Vector3(AmoFired.transform.localScale.x * -1, AmoFired.transform.localScale.y, transform.localScale.z);
                rigidbody.velocity = Rigidbody.velocity;
                rigidbody.AddForce(new Vector2(-rigidbody.mass * 10f, 0), ForceMode2D.Impulse);
            }
            else
            {
                Vector3 pos = transform.position;
                pos.x += 0.5f;
                GameObject AmoFired = Instantiate(ammo, pos, Quaternion.identity);
                Rigidbody2D rigidbody = AmoFired.GetComponent<Rigidbody2D>();
                rigidbody.velocity = Rigidbody.velocity;
                rigidbody.AddForce(new Vector2(rigidbody.mass * 10f, 0), ForceMode2D.Impulse);
            }

        }
    }

    private void checkIfDead()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Death");
            dead = true;
        }

    }
    public void setLastCheckpoint(Vector2 pos)
    {
        lastCheckpoint = pos;
    }
}
