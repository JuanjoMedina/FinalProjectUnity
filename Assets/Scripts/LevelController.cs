using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int index;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator delayCoroutine(Collider2D collision)
    {
        yield return new WaitForSeconds(2.0f);
        collision.transform.position = new Vector3(0, 0, 0);
        ((Player)collision.GetComponent<MonoBehaviour>()).setLastCheckpoint(new Vector2(0, 0));
        SceneManager.LoadScene(index);
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
