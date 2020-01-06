using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private float frames_blocked;
    private bool dead;
    private Animator animator;
    private new Rigidbody2D rigidbody;


    // Start is called before the first frame update
    void Awake()
    {
        Health = 80f;
        dead = false;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!dead)
        {
            CheckIfDead();
            if (!dead)
            {
                //TryToCatch();
            }
        }

    }
    public override void Damage(float damage)
    {
        Health -= 20;
    }
    private void CheckIfDead()
    {
        if (Health <= 0)
        {
            dead = true;
            rigidbody.velocity = new Vector2(0, 0);
            rigidbody.gravityScale = 0;
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(transform.GetChild(0).gameObject);
            animator.SetTrigger("Dead");
            Destroy(gameObject,5f);
        }

    }
    private void TryToCatch()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float playerx = player.transform.position.x;
        float enemyx = this.transform.position.x;
        if (Mathf.Abs(playerx - enemyx) < 10)
        {
            if (playerx < enemyx)
            {
                rigidbody.AddForce(new Vector2(-rigidbody.mass * 5, 0), ForceMode2D.Force);
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
                animator.SetTrigger("Walk");
            }
            if (playerx > enemyx)
            {
                rigidbody.AddForce(new Vector2(rigidbody.mass * 5, 0), ForceMode2D.Force);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                animator.SetTrigger("Walk");
            }
            if (rigidbody.velocity.x <=Mathf.Pow(10,-8) && rigidbody.velocity.y <= Mathf.Pow(10, -8))
            {
                float epsilon = Mathf.Epsilon;
                frames_blocked++;
                if (frames_blocked == 30)
                {
                    frames_blocked = 0;
                    rigidbody.AddForce(new Vector2(0, rigidbody.mass * 7), ForceMode2D.Impulse);
                }
            }
            else
            {
                frames_blocked = 0;
            }
        }
    }
    public bool getDead()
    {
        return dead;
    }
}
