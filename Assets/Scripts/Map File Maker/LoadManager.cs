using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class LoadManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StreamReader streamReader = new StreamReader("NewMap.txt");
        string text;
        List<string[][]> relationVec = new List<string[][]>();
        while ((text = streamReader.ReadLine()) != null)
        {
            string[] objectString = text.Split(' ');
            GameObject go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + objectString[0].Split('(')[0] + ".prefab", typeof(Object)) as GameObject;
            GameObject instance = Instantiate(go, new Vector3(float.Parse(objectString[1]), float.Parse(objectString[2]), float.Parse(objectString[3])), Quaternion.Euler(new Vector3(float.Parse(objectString[7]), float.Parse(objectString[8]), float.Parse(objectString[9]))));
            instance.name = objectString[0];
            instance.transform.localScale = new Vector3(float.Parse(objectString[4]), float.Parse(objectString[5]), float.Parse(objectString[6]));
            if (objectString.Length > 10)
            {
                instance.AddComponent<AutodestroyIfKilled>();
                if (instance.GetComponent<MonoBehaviour>().GetType().Name.Equals("AutodestroyIfKilled"))
                {
                    AutodestroyIfKilled script = instance.GetComponent<MonoBehaviour>() as AutodestroyIfKilled;
                    relationVec.Add(new string[2][]);
                    relationVec[relationVec.Count-1][0] = new string[1] { objectString[0] };
                    relationVec[relationVec.Count-1][1] = new string[objectString.Length - 10];
                    for (int i = 10; i < objectString.Length; i++)
                    {
                        relationVec[0][1][i - 10] = objectString[i];
                    }
                }

            }
            
        }
        for(int i=0;i< relationVec.Count;i++)
        {
            AutodestroyIfKilled script = GameObject.Find(relationVec[i][0][0]).GetComponent<MonoBehaviour>() as AutodestroyIfKilled;
            script.killedObjects = new GameObject[relationVec[i][1].Length];
            foreach (string name in relationVec[i][1])
            {
                script.killedObjects[i] = GameObject.Find(name);
            }
        }

        ((Camera)GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MonoBehaviour>()).player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
