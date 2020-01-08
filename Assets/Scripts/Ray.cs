using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    private Animator animator;
    private float damage;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetTrigger("Hit");
        if (collision.tag.Equals("Player") || collision.tag.Equals("Enemy"))
            ((Character)collision.GetComponent<MonoBehaviour>()).Damage(damage);
    }
}
