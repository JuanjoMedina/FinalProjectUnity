using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    public UnityEngine.Camera cam;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(cam.orthographicSize * cam.aspect * 2f, cam.orthographicSize * 2f);
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.sizeDelta = new Vector2(cam.orthographicSize * cam.aspect * 2f, cam.orthographicSize * 2f);
    }
}
