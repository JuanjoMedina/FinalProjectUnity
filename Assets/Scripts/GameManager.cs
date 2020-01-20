using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http;
using UnityEngine.Networking;
using UnityEditor;
using Assets.Scripts.Api_Classes;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    private int currentMap;
    private string current_map_data;
    private static HttpClient client = new HttpClient();
    private bool initialize = false;
    private bool loaded;
    private GameObject player;
    public User usuario;

    public int CurrentMap { get => currentMap; set => currentMap = value; }
    public string Current_map_data { get => current_map_data; set => current_map_data = value; }

    void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
            try
            {
                StartCoroutine(getUser());
                StartCoroutine(getCurrentProgress());
            }
            catch
            {
                Debug.Log("Connection Error");
            }
        }
        else
            Destroy(gameObject);

    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (initialize)
        {
            StartCoroutine(downloadMap());
            initialize = false;
        }
        if (loaded)
        {
            player.transform.position = new Vector3(usuario.xPos, usuario.yPos, usuario.zPos);
            ((Player)player.GetComponent<MonoBehaviour>()).setLastCheckpoint(new Vector2(usuario.xPos, usuario.yPos));
            loaded = false;
        }
            
    }
    public void startDownloading()
    {
        StartCoroutine(downloadMap());
    }
    public IEnumerator downloadMap()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://147.83.7.206:8080/dsaApp/map/"+currentMap.ToString());
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Current_map_data = JsonUtility.FromJson<Map>(www.downloadHandler.text).data;
            loadMap(Current_map_data);
            loaded = true;
        }
    }
    public IEnumerator getCurrentProgress()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://147.83.7.206:8080/dsaApp/getPosition/pcf3333");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            CurrentProgress curr=JsonUtility.FromJson<CurrentProgress>(www.downloadHandler.text);
            CurrentMap = curr.lastMap;
            initialize = true;

        }
    }

    public void loadMap(string map)
    {
        string[] lineas = map.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        List<string[][]> relationVec = new List<string[][]>();
        foreach (string text in lineas)
        {
            if (!text.Equals(""))
            {
                string[] objectString = text.Split(' ');
                GameObject go = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/" + objectString[0].Split('(')[0] + ".prefab", typeof(UnityEngine.Object)) as GameObject;
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
                        relationVec[relationVec.Count - 1][0] = new string[1] { objectString[0] };
                        relationVec[relationVec.Count - 1][1] = new string[objectString.Length - 10];
                        for (int i = 10; i < objectString.Length; i++)
                        {
                            relationVec[0][1][i - 10] = objectString[i];
                        }
                    }

                }
            }

        }
        for (int i = 0; i < relationVec.Count; i++)
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

    public IEnumerator updateProgress()
    {
        UnityWebRequest www = UnityWebRequest.Put("http://147.83.7.206:8080/dsaApp/modifyUser", System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(usuario)));
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }

    }
    private IEnumerator getUser()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://147.83.7.206:8080/dsaApp/users/pcf3333");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            usuario = JsonUtility.FromJson<User>(www.downloadHandler.text);
        }
    }
    public void startUpdateProgress()
    {
        StartCoroutine(updateProgress());
    }

}
