using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
    private Dictionary<string, UnityEngine.Object> dictionary_Resources = new Dictionary<string, Object>();

    public T Load<T>(string key) where T : Object
    {
        if(dictionary_Resources.TryGetValue(key,out Object resource))  //키를 통해서 찾아보고 있다면 나온 반환을 다시 새로 만든 지역 변수 안에 넣는 느낌인 듯?
        {
            return dictionary_Resources as T;
        }

        if(typeof(T) == typeof(Sprite))                     //만약 스프라이트라면 키를 바꿔서 저장했으니까 스프라이트인지 확인한 후 
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
