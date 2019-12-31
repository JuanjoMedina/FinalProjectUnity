using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Character
{
    private float health;
    private Animator animator;
    private bool dead;

    // Start is called before the first frame update
    void Awake()
    {
        dead = false;
        health = 200f;
        animator = GetComponent<Animator>();
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
    void checkIfDead()
    {
        if(health<=0 && !dead)
        {
            animator.SetTrigger("Death");
            dead = true;
        }
    }
}
