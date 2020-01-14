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
        while ((text = streamReader.ReadLine()) != null)
        {
            string[] objectString = text.Split(' ');
            GameObject instance;
            if (objectString.Length > 10)
            {
                GameObject go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + objectString[0].Split('(')[0] + ".prefab", typeof(Object)) as GameObject;
                instance=Instantiate(go, new Vector3(float.Parse(objectString[1]), float.Parse(objectString[2]), float.Parse(objectString[3])), Quaternion.Euler(new Vector3(float.Parse(objectString[7]), float.Parse(objectString[8]), float.Parse(objectString[9]))));
            }
            else
            {
                GameObject go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + objectString[0].Split('(')[0] + ".prefab", typeof(Object)) as GameObject;
                instance=Instantiate(go, new Vector3(float.Parse(objectString[1]), float.Parse(objectString[2]), float.Parse(objectString[3])), Quaternion.Euler(new Vector3(float.Parse(objectString[7]), float.Parse(objectString[8]), float.Parse(objectString[9]))));
            }
            instance.transform.localScale = new Vector3(float.Parse(objectString[4]), float.Parse(objectString[5]), float.Parse(objectString[6]));
        }
        ((Camera)GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MonoBehaviour>()).player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
