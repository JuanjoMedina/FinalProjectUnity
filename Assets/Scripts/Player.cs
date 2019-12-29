using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
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
        health = 100f;
        jetpack = false;
        dead = false;
        this.Rigidbody = GetComponent<Rigidbody2D>();
        this.BoxCollider2D = GetComponent<BoxCollider2D>();
        this.animator = GetComponent<Animator>();
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            animator.SetTrigger("Jetpack_Stop");
            jetpack = false;
        }
    }

    public override void damage(float damage)
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
                walking = true;
            }
            this.Rigidbody.AddRelativeForce(new Vector2(Rigidbody.mass * 9.81f * -0.6f, 0), ForceMode2D.Force);
            this.transform.localScale = new Vector2(-0.1f, 0.1f);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (!jetpack)
            {
                animator.SetTrigger("Walk");
                walking = true;
            }
            this.Rigidbody.AddRelativeForce(new Vector2(Rigidbody.mass * 9.81f * 0.6f, 0), ForceMode2D.Force);
            this.transform.localScale = new Vector2(0.1f, 0.1f);

        }
        if (!walking && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            animator.SetTrigger("Stop_Walking");
    }
    private void checkIfDead()
    {
        if (health <= 0)
        {
            animator.SetTrigger("Death");
            dead = true;
        }

    }
    
}
