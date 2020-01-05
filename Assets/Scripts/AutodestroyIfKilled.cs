using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutodestroyIfKilled : MonoBehaviour
{
    public GameObject[] killedObjects;
    private GameObject hola = null;
    // Update is called once per frame
    void Update()
    {
        int n = 0;
        foreach(GameObject gameobject in killedObjects)
        {
            if (gameobject == null || gameobject.Equals("null"))
                n++;
            else
            {
                if (gameobject.tag.Equals("Enemy"))
                {
                    MonoBehaviour script = gameobject.GetComponent<MonoBehaviour>();
                    if (script.GetType().Name.Equals("Enemy"))
                        if (((Enemy)script).getDead())
                            n++;
                }
            }
        }
        if (killedObjects.Length == n)
            Destroy(gameObject);
    }
}
