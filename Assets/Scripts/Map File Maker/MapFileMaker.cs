using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.IO;

public class MapFileMaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        GameObject[] gameObjects1 = GameObject.FindGameObjectWithTag("Player").scene.GetRootGameObjects();
        List<GameObject> list = new List<GameObject>();
        list.AddRange(gameObjects);
        list.AddRange(gameObjects1);
        gameObjects = list.ToArray();
        StreamWriter streamWriter = new StreamWriter("NewMap.txt");

        foreach (GameObject go in gameObjects)
        {
            string gameObjectName = go.name.Replace(" ","");
            //string gameObjectName = a.Split(new string[] { " (" },System.StringSplitOptions.None)[0];
            List<string> targets = new List<string>(); 
            if (!gameObjectName.Equals("MapFileMaker")) {
                foreach (MonoBehaviour script in go.GetComponents<MonoBehaviour>())
                {
                    if (script.GetType().Name.Equals("AutodestroyIfKilled"))
                    {
                        foreach (GameObject target in ((AutodestroyIfKilled)script).killedObjects )
                        {
                            targets.Add(target.name.Replace(" ",""));
                        }
                    }
                        
                }
                streamWriter.Write(gameObjectName + " " + go.transform.position.x.ToString() + " " +
                    go.transform.position.y.ToString() + " " + go.transform.position.z.ToString() + " " +
                    go.transform.localScale.x.ToString() + " " + go.transform.localScale.y.ToString() + " " +
                    go.transform.localScale.z.ToString() + " " + go.transform.rotation.eulerAngles.x.ToString() + " " +
                    go.transform.rotation.eulerAngles.y.ToString() + " " + go.transform.rotation.eulerAngles.z.ToString());

                foreach (string target in targets)
                    streamWriter.Write(" " + target);

                streamWriter.Write("\r\n");
            }
        }
        streamWriter.Close();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
