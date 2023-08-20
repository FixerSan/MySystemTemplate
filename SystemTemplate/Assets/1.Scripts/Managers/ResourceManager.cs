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
        #region ����ó��
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

    //�ε�â ��� �뵵
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

    //�̹� �ε� �� ��(��ųʸ�)�� �̾ƿ� ��
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
        //�������� key�� ������ �ε� �� �ε� �� ���� ��ųʸ��� �ְ� �ݹ�
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

        //�ƴ� ��� �ε�Ǿ� �ִ��� üũ �� �Ǿ� �ִٸ� �̾Ƽ� �ν��Ͻ� �� ����
        //�ε尡 �Ǿ� ���� �ʴٸ� ���� �ε� �� 
        GameObject prefab = CheckLoaded<GameObject>($"{_key}");
        if (prefab == null)
        {
            Debug.LogError("�������� �ε�Ǿ� ���� ����, �ε� �ϼ�");
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
