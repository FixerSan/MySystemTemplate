using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    #region Singleton
    private static SceneManager instance;
    public static SceneManager Instance { get { return instance; } }

    [RuntimeInitializeOnLoadMethod]
    private static void Init()
    {
        GameObject go = GameObject.Find($"[{nameof(SceneManager)}]");
        if(go == null)
            go = new GameObject { name = $"[{nameof(SceneManager)}]" };
        instance = go.GetOrAddComponent<SceneManager>();
        DontDestroyOnLoad(go);
    }
    #endregion

    private void Awake()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += LoadedScene;
    }
    private string currentScene = string.Empty;

    public void LoadScene(Define.Scene _scene)
    {
        string sceneName = _scene.ToString();
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void LoadedScene(UnityEngine.SceneManagement.Scene _scene, UnityEngine.SceneManagement.LoadSceneMode _loadSceneMode)
    {
        RemoveScene(currentScene, () => 
        {
            currentScene = _scene.name;
            AddScene(_scene.name);
        });
    }

    public void AddScene(string _sceneName)
    {
        Define.Scene addScene = Util.ParseEnum<Define.Scene>(_sceneName);
        switch(addScene)
        {
            case Define.Scene.Loading:
                gameObject.AddComponent<LoadingScene>();
                break;

            case Define.Scene.Guild:
                gameObject.AddComponent<GuildScene>();
                break;

            case Define.Scene.IceDungeon:
                gameObject.AddComponent<IceDungeonScene>();
                break;
        }
    }

    public void RemoveScene(string _sceneName, System.Action _callback = null)
    {
        if(string.IsNullOrEmpty(_sceneName))
        {
            _callback?.Invoke();
            return;
        }

        Define.Scene addScene = Util.ParseEnum<Define.Scene>(_sceneName);
        switch (addScene)
        {
            case Define.Scene.Loading:
                Destroy(gameObject.GetComponent<LoadingScene>());
                break;

            case Define.Scene.Guild:
                Destroy(gameObject.GetComponent<GuildScene>());
                break;

            case Define.Scene.IceDungeon:
                Destroy(gameObject.GetComponent<IceDungeonScene>());
                break;
        }
        _callback?.Invoke();
    }

    public string GetCurrentSceneName()
    {
        return currentScene;
    }
}
