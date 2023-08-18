using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager
{
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
                Managers.Instance.gameObject.AddComponent<LoadingScene>();
                break;

            case Define.Scene.Guild:
                Managers.Instance.gameObject.AddComponent<GuildScene>();
                break;

            case Define.Scene.IceDungeon:
                Managers.Instance.gameObject.AddComponent<IceDungeonScene>();
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
                Object.Destroy(Managers.Instance.gameObject.GetComponent<LoadingScene>());
                break;

            case Define.Scene.Guild:
                Object.Destroy(Managers.Instance.gameObject.GetComponent<GuildScene>());
                break;

            case Define.Scene.IceDungeon:
                Object.Destroy(Managers.Instance.gameObject.GetComponent<IceDungeonScene>());
                break;
        }
        _callback?.Invoke();
    }

    public string GetCurrentSceneName()
    {
        return currentScene;
    }
}
