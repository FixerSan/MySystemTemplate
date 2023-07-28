using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
    private Dictionary<string, UnityEngine.Object> dictionary_Resources = new Dictionary<string, Object>();

    public T Load<T>(string key) where T : Object
    {
        if(dictionary_Resources.TryGetValue(key,out Object resource))  //Ű�� ���ؼ� ã�ƺ��� �ִٸ� ���� ��ȯ�� �ٽ� ���� ���� ���� ���� �ȿ� �ִ� ������ ��?
        {
            return dictionary_Resources as T;
        }

        if(typeof(T) == typeof(Sprite))                     //���� ��������Ʈ��� Ű�� �ٲ㼭 ���������ϱ� ��������Ʈ���� Ȯ���� �� 
        {
            key = key + ".sprite";
            if(dictionary_Resources.TryGetValue(key,out Object sprite))
            {
                return sprite as T;
            }
        }

        return null;
    }

    public GameObject Instantiate(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = Load<GameObject>($"{key}");
        if (prefab == null)
        {
            Debug.LogError($"Resource Manager Fail Load : {key}");
            return null;
        }

        if (pooling)
            return Managers.Pool.Get(key);
        return null;

    }
}
