using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Character
{
    public GameObject Shot;
    private Animator animator;
    private bool dead;
    private float timer;
    private int shotNum;
    private int numOfShots;
    private int numOfPause;
    private bool detected;
    private bool shooting;

    // Start is called before the first frame update
    void Awake()
    {
        dead = false;
        Health = 200f;
        animator = GetComponent<Animator>();
        timer = 0;
        shotNum = 0;
        detected = false;
        shooting = true;
        numOfShots = 8;
        numOfPause = 4;
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x;
        if (!dead && Mathf.Abs(distance) < 10)
        {
            if (!detected)
            {
                detected = true;
                animator.SetTrigger("Shoot");
            }
            if (timer > 0.5)
            {
                shotNum++;
                timer = 0;
                if (shooting)
                {
                    shoot();
                }
                if (shotNum == numOfShots && shooting)
                {
                    shooting = false;
                    animator.SetTrigger("Stop_Shooting");
                    shotNum = 0;
                }
                else if (shotNum == numOfPause && !shooting)
                {
                    shooting = true;
                    animator.SetTrigger("Shoot");
                    shotNum = 0;
                }
                
            }
            else
                timer += Time.deltaTime;
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                animator.SetTrigger("Stop_Shooting");
            detected = false;
            shooting = true;
            timer = 0;
            shotNum = 0;
        }
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
        pos.y += 0.2f;
        if (transform.localScale.x > 0)
        {
            pos.x -= 0.7f;
            GameObject shot = Instantiate(Shot, pos, Quaternion.identity);
            Rigidbody2D rigidbody = shot.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(new Vector2(-10, 0), ForceMode2D.Impulse);
        }
        else
        {
            pos.x += 0.7f;
            GameObject shot = Instantiate(Shot, pos, Quaternion.identity);
            Rigidbody2D rigidbody = shot.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(new Vector2(10, 0), ForceMode2D.Impulse);
        }

    }
}
