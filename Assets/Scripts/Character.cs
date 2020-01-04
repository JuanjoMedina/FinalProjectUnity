using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private float health;

    public float Health { get => health; set => health = value; }

    public abstract void Damage(float damage);
}
