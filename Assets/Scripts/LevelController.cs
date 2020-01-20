using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
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
        GameManager.gameManager.CurrentMap += 1;
        foreach (GameObject go in SceneManager.GetActiveScene().GetRootGameObjects())
            Destroy(go);
        GameManager.gameManager.startDownloading();
        Assets.Scripts.Api_Classes.User usuario = GameManager.gameManager.usuario;
        usuario.xPos = 0;
        usuario.yPos = 0;
        usuario.zPos = 0;
        GameManager.gameManager.startUpdateProgress();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.Play("exit");
            StartCoroutine(delayCoroutine(collision));
        }
    }
}
