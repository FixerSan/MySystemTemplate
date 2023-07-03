using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private Dictionary<string, BaseScene> currentScenes = new Dictionary<string, BaseScene>();
    private string currentSceneName;

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnLoadScene;
        if(System.String.IsNullOrEmpty(currentSceneName))   currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        RegistScene(currentSceneName);
    }

    private void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnLoadScene;
        UnLoadSceneAll();
    }

    private void OnLoadScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(currentSceneName != null)
        {
            UnloadScene(currentSceneName);
        }
        currentSceneName = scene.name;
        RegistScene(currentSceneName);
    }

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void RegistScene(string sceneName)
    {
        BaseScene tempScene;

        switch(sceneName)
        {
            case "Ice_00":
                tempScene = gameObject.AddComponent<Ice_00_Scene>();
                break;

            case "Ice_01":
                tempScene = gameObject.AddComponent<Ice_01_Scene>();
                break;

            case "Ice_02":
                tempScene = gameObject.AddComponent<Ice_02_Scene>();
                break;

            case "Fire_00":
                tempScene = gameObject.AddComponent<Ice_02_Scene>();
                break;

            case "Fire_01":
                tempScene = gameObject.AddComponent<Ice_02_Scene>();
                break;

            case "Fire_02":
                tempScene = gameObject.AddComponent<Ice_02_Scene>();
                break;

            default:
                tempScene = null;
                break;
        }

        if (tempScene != null && !currentScenes.ContainsKey(sceneName))
        {
            currentScenes.Add(sceneName,tempScene);
        }
    }

    public void UnloadScene(string sceneName)
    {
        if (currentScenes.ContainsKey(sceneName))
        {
            BaseScene tempScene = currentScenes[sceneName];
            currentScenes.Remove(sceneName);
            Destroy(tempScene);
        }
    }

    public void UnLoadSceneAll()
    {
        List<string> keyList = new List<string>();

        foreach (var key in currentScenes)
        {
            keyList.Add(key.Key);
        }

        foreach (var key in keyList)
        {
            UnloadScene(key);
        }
    }

    public string GetCurrentSceneName()
    {
        return currentSceneName;
    }

    public BaseScene GetCurrentScene()
    {
        if (currentScenes.ContainsKey(currentSceneName))    return currentScenes[currentSceneName];
        return null;
    }
}
