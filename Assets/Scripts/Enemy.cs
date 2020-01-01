using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private float health;
    private bool dead;
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Awake()
    {
        health = 80f;
        dead = false;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkIfDead();
        tryToCatch();
    }
    public override void damage(float damage)
    {
        health -= 20;
    }
    private void checkIfDead()
    {
        if (health <= 0)
        {
            dead = true;
            Destroy(gameObject);
        }

    }
    private void tryToCatch()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float playerx = player.transform.position.x;
        float enemyx = this.transform.position.x;
        if (Mathf.Abs(playerx - enemyx) < 10)
        {
            if (playerx < enemyx)
            {
                rigidbody.AddForce(new Vector2(-rigidbody.mass * 5, 0), ForceMode2D.Force);
            }
            if (playerx > enemyx)
            {
                rigidbody.AddForce(new Vector2(rigidbody.mass * 5, 0), ForceMode2D.Force);
            }
        }
    }
}
