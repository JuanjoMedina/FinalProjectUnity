using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private static Camera instance;
    public GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    void Start()
    {
        this.transform.position = player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y, -30);
        this.transform.position = pos;
    }
}
