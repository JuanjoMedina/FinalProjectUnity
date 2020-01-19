using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public bool paused;
    private GameObject Image;
    // Start is called before the first frame update
    void Start()
    {
        Image = GameObject.Find("Image");
        Image.SetActive(true);
        paused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            paused = false;
        }
        if (paused)
        {
            Time.timeScale = 0;
        }
        else if (!paused)
        {
            Time.timeScale = 1;
            StartCoroutine(delayCoroutine());
        }
    }
    IEnumerator delayCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        Image.SetActive(false);
    }
}
