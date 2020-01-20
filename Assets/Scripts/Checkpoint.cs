using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ((Player)GameObject.FindGameObjectWithTag("Player").GetComponent<MonoBehaviour>()).setLastCheckpoint(transform.position);
            Assets.Scripts.Api_Classes.User usuario = GameManager.gameManager.usuario;
            usuario.xPos = (int) transform.position.x;
            usuario.yPos = (int) transform.position.y;
            GameManager.gameManager.startUpdateProgress();
        }
            
    }
}
