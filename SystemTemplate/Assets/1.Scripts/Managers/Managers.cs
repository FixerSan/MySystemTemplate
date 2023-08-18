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
            if (instance == null)
                Init();
            return instance;
        }
    }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        if(instance != null)
           return;

        GameObject go = GameObject.Find("@Managers");
        if (go == null)
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
        }

        DontDestroyOnLoad(go);
        instance = go.GetOrAddComponent<Managers>();
    }

    private void Awake()
    {
        if(instance == null)
            Init();
        else
            Destroy(gameObject);
    }
    #endregion
    //매니저를 추가할 자리
    private ResourceManager resource = new ResourceManager();
    private PoolManager pool = new PoolManager();
    private UIManager ui = new UIManager();

    public static ResourceManager Resource { get { return Instance?.resource; } }
    public static PoolManager Pool { get { return Instance?.pool; } }
    public static UIManager UI { get { return Instance?.ui; } }
    public static CoroutineManager Routine{ get { return CoroutineManager.Instance; } }
}
