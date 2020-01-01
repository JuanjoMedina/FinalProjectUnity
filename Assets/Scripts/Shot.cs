using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Shot : MonoBehaviour
{

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject.Destroy(gameObject, 2);
    }

}
