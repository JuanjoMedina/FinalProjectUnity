using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour
{
    private Character character;
    void Start()
    {
        character=(Character)transform.parent.GetComponent<MonoBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Enemy")
            character.damage(20f);
    }
}
