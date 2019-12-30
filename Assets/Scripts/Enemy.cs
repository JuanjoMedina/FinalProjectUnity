using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private float health;
    private bool dead;

    // Start is called before the first frame update
    void Awake()
    {
        health = 80f;
        dead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkIfDead();
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
}
