using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Character
{
    private float health;

    // Start is called before the first frame update
    void Awake()
    {
        health = 200f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void damage(float damage)
    {
        health -= 20;
    }
}
