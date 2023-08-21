using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class ResourceManager 
{
    private Dictionary<string, Object> resourceDictionary = new Dictionary<string, Object>();
    private Dictionary<string, Object> preResourceDictionary = new Dictionary<string, Object>();

    public void Load<T>(string _key, Action<T> _callback = null) where T : Object
    {
        T ob = CheckLoaded<T>(_key);

        if (ob != null)
        {
            _callback?.Invoke(ob as T);
            return;
        }

        LoadAsync<T>(_key, (_ob) =>
        {
            _callback?.Invoke(_ob as T);
        });
    }

    //로딩창 사용 용도
    public void LoadAllAsync<T>(string _label, Action<string,int,int> _callback = null) where T : Object
    {
        var operationHandle = Addressables.LoadResourceLocationsAsync(_label, typeof(T));

        operationHandle.Completed += (op) => 
        {
            int currentLoadCount = 0;
            int totalLoadCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                LoadAsync<T>(result.PrimaryKey, (ob) => 
                {
                    currentLoadCount++;
                    _callback?.Invoke(result.PrimaryKey, currentLoadCount, totalLoadCount);
                });
            }
        };
    }

    //이미 로드 된 것(딕셔너리)을 뽑아올 때
    private T CheckLoaded<T>(string _key) where T : Object
    {
        string loadKey = ChangeKey<T>(_key);

        if (resourceDictionary.TryGetValue(loadKey, out Object resource))
        {
            return resource as T;
        }

        if (preResourceDictionary.TryGetValue(loadKey, out Object preResource))
        {
            return preResource as T;
        }
        return null;
    }

    private void LoadAsync<T>(string _key, Action<T> _callback = null) where T : Object
    {
        string loadKey = ChangeKey<T>(_key);

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
        asyncOperation.Completed += (op) => 
        {
            if(!resourceDictionary.ContainsKey(loadKey))
                resourceDictionary.Add(loadKey, op.Result);
            _callback?.Invoke(op.Result as T);
        };
    }

    public void PreResourceLoad(Action _callback = null)
    {
        var operationHandle = Addressables.LoadResourceLocationsAsync("Preload");

        operationHandle.Completed += (op) =>
        {
            int currentLoadCount = 0;
            int totalLoadCount = op.Result.Count;

            foreach (var result in op.Result)
            {
                var asyncOperation = Addressables.LoadAssetAsync<Object>(result.PrimaryKey);
                asyncOperation.Completed += (op) =>
                {
                    currentLoadCount++;
                    if (!preResourceDictionary.ContainsKey(result.PrimaryKey))
                        preResourceDictionary.Add(result.PrimaryKey, op.Result);
                    if (currentLoadCount == totalLoadCount)
                        _callback.Invoke();
                };
            }
        };
    }

    public GameObject Instantiate(string _key, Transform _parent = null, bool _pooling = false)
    {
        GameObject prefab = CheckLoaded<GameObject>($"{_key}");
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {_key}");
            return null;
        }

        if (_pooling)
            return Managers.Pool.Get(prefab);

        GameObject go = Object.Instantiate(prefab, _parent);

        go.name = prefab.name;
        return go;
    }

    public void Destroy(GameObject _go)
    {
        if (_go == null) return;
        if (Managers.Pool.Push(_go)) return;

        Object.Destroy(_go);
    }

    private string ChangeKey<T>(string _key) where T : Object
    {
        if (typeof(T) == typeof(TextAsset)) _key = _key + ".Data";
        if (typeof(T) == typeof(GameObject)) _key = _key + ".GameObject";

        return _key;
    }
}
