using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region Singleton
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
            go = new GameObject { name = "@Managers" };
        instance = go.GetOrAddComponent<Managers>();

        DontDestroyOnLoad(go);
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
    public static CoroutineManager Routine { get { return CoroutineManager.Instance; } }

    public static SceneManager scene { get { return SceneManager.Instance; } }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            Managers.scene.LoadScene(Define.Scene.Guild);
        }
    }
}
