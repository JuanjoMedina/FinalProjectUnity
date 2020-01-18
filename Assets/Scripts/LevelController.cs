using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int index;

    public float xPosition;
    public float yPosition;
    public float zPosition;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //StartCoroutine(Coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator delayCoroutine(Collider2D collision)
    {
        yield return new WaitForSeconds(2.0f);
        collision.transform.position = new Vector3(xPosition, yPosition, zPosition);
        //DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(index);
        Destroy(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.Play("exit");
            StartCoroutine(delayCoroutine(collision));

            //SceneManager.LoadScene(levelName);
        }
    }
}
