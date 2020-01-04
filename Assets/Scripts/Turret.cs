using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Character
{
    private Animator animator;
    private bool dead;

    // Start is called before the first frame update
    void Awake()
    {
        dead = false;
        Health = 200f;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkIfDead();
    }
    public override void Damage(float damage)
    {
        Health -= 20;
    }
    void checkIfDead()
    {
        if(Health<=0 && !dead)
        {
            animator.SetTrigger("Death");
            dead = true;
        }
    }
}
