using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    #region Singleton, DontDestoryOnLoad, Awake
    private static Manager instance;
    public static Manager Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            return null;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public SceneManager scene;          //纠 包府 概聪历
    public WorldManager world;          //岿靛 甘 包府 概聪历
    public Database database;           //单捞磐海捞胶
    public UISystem ui;                 //UI贸府 包府 概聪历

    public void Setup()
    {
        database = gameObject.AddComponent<Database>();
        scene = gameObject.AddComponent<SceneManager>();
        world = gameObject.AddComponent<WorldManager>();
        ui = gameObject.AddComponent<UISystem>();
    }

    private void OnEnable()
    {
        Setup();
    }

    private void OnDisable()
    {
        Destroy(GetComponent<Database>());
        Destroy(GetComponent<SceneManager>());
        Destroy(GetComponent<WorldManager>());
        Destroy(GetComponent<UISystem>());
    }
}
