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
        string type = character.GetType().Name;
        if (type.Equals("Player"))
        {
            if (collision.gameObject.tag.Equals("Enemy"))
                character.damage(20f);
        }
        if (type.Equals("Enemy"))
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                character.damage(20f);
            }

            if(collision.gameObject.tag.Equals("Shot"))
            {
                Destroy(collision.gameObject);
                character.damage(20);
            }
        }
        if (type.Equals("Turret"))
        {

            if (collision.gameObject.tag.Equals("Shot"))
            {
                Destroy(collision.gameObject);
                character.damage(20);
            }
        }

    }
}
