using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region Sington
    private static Managers instance;
    public static Managers Instance
    {
        get 
        {
            Init();
            return instance;
        }
    }

    private static void Init()
    {
        if(!instance)
        {
            GameObject go = GameObject.Find("@Managers");
            if(!go)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();
        }
    }
    #endregion
    //매니저를 추가할 자리
    private ResourceManager resource = new ResourceManager();
    private PoolManager pool = new PoolManager();
    public static ResourceManager Resource { get { return Instance?.resource; } }
    public static PoolManager Pool { get { return Instance?.pool; } }
}
