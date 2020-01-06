using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Character
{
    public GameObject Shot;
    private Animator animator;
    private bool dead;
    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        dead = false;
        Health = 200f;
        animator = GetComponent<Animator>();
        timer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x;
        if (!dead && Mathf.Abs(distance) < 10)
            if (timer > 0.5)
            {
                animator.SetTrigger("Shoot");
                timer = 0;
                shoot();
            }
            else
                timer += Time.deltaTime;
        else
            animator.SetTrigger("Stop_Shooting");
    }
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
    void shoot()
    {
        Vector3 pos=transform.position;
        pos.x -= 0.1f;
        GameObject shot = Instantiate(Shot, pos, Quaternion.identity);
        Rigidbody2D rigidbody= shot.GetComponent<Rigidbody2D>();
        if (transform.localScale.x>0)
            rigidbody.AddForce(new Vector2(-10, 0),ForceMode2D.Impulse);
        else
            rigidbody.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);

    }
}
