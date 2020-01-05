using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject healthObject;

    private float MaxHealth;
    // Start is called before the first frame update
    void Start()
    {
        if (healthObject == null)
            healthObject = GameObject.FindGameObjectWithTag("Player");
        MaxHealth = ((Character)healthObject.GetComponent<MonoBehaviour>()).Health;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = ((Character)healthObject.GetComponent<MonoBehaviour>()).Health/MaxHealth;
        if (scale.x < 0)
            scale.x = 0;
        transform.localScale = scale;
    }
}
