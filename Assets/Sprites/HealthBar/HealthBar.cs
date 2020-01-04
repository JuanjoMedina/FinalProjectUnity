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
        MaxHealth = ((Character)healthObject.GetComponent<MonoBehaviour>()).Health;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = transform.localScale;
        scale.x = ((Character)healthObject.GetComponent<MonoBehaviour>()).Health/MaxHealth;
        transform.localScale = scale;
    }
}
