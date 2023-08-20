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
        string loadKey = _key;
        #region 예외처리
        if (typeof(T) == typeof(TextAsset))
            loadKey = _key + "Data";
        #endregion

        T ob = CheckLoaded<T>(loadKey);

        if (ob != null)
        {
            _callback?.Invoke(ob as T);
            return;
        }

        LoadAsync<T>(loadKey, (_ob) =>
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
        if (resourceDictionary.TryGetValue(_key, out Object resource))
        {
            return resource as T;
        }

        if (preResourceDictionary.TryGetValue(_key, out Object preResource))
        {
            return preResource as T;
        }
        return null;
    }

    private void LoadAsync<T>(string _key, Action<T> _callback = null) where T : Object
    {
        //최종적인 key를 가지고 로드 후 로드 된 값을 딕셔너리에 넣고 콜백
        var asyncOperation = Addressables.LoadAssetAsync<T>(_key);
        asyncOperation.Completed += (op) => 
        {
            if(!resourceDictionary.ContainsKey(_key))
                resourceDictionary.Add(_key, op.Result);
            _callback?.Invoke(op.Result as T);
        };
    }

    public void PreResourceLoad()
    {
        var operationHandle = Addressables.LoadResourceLocationsAsync("Preload");

        operationHandle.Completed += (op) =>
        {
            foreach (var result in op.Result)
            {
                var asyncOperation = Addressables.LoadAssetAsync<Object>(result.PrimaryKey);
                asyncOperation.Completed += (op) =>
                {
                    if (!resourceDictionary.ContainsKey(result.PrimaryKey))
                        resourceDictionary.Add(result.PrimaryKey, op.Result);
                };
            }
        };
    }

    public GameObject Instantiate(string _key, Transform _parent = null, bool _pooling = false)
    {
        GameObject po = Managers.Pool.Get(_key);
        if (po != null)
        {
            po.transform.SetParent(_parent);
            return po;
        }

        //아닐 경우 로드되어 있는지 체크 후 되어 있다면 뽑아서 인스턴스 후 리턴
        //로드가 되어 있지 않다면 새로 로드 후 
        GameObject prefab = CheckLoaded<GameObject>($"{_key}");
        if (prefab == null)
        {
            Debug.LogError("프리팹이 로드되어 있지 않음, 로드 하셈");
            return null;
        }

        GameObject go = GameObject.Instantiate(prefab);
        go.name = prefab.name;
        go.transform.SetParent(_parent);
        return go;
    }

    public void Destroy(GameObject _go)
    {
        if (_go == null) return;
        if (Managers.Pool.Push(_go)) return;

        Object.Destroy(_go);
    }
}
