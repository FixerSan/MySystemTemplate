using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;

public class ResourceManager 
{
    private Dictionary<string, UnityEngine.Object> dictionary_Resources = new Dictionary<string, UnityEngine.Object>();

    public void Load<T>(string _key, Action<T> _callback = null) where T : UnityEngine.Object
    {
        T ob = Load<T>(_key);

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

    //�ε�â ��� �뵵
    public void LoadAllAsynk<T>(string _label, Action<string,int,int> _callback = null) where T : UnityEngine.Object
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
    private T Load<T>(string _key) where T : UnityEngine.Object
    {
        if (dictionary_Resources.TryGetValue(_key, out UnityEngine.Object resource))  //Ű�� ���ؼ� ã�ƺ��� �ִٸ� ���� ��ȯ�� �ٽ� ���� ���� ���� ���� �ȿ� �ִ� ������ ��?
        {
            return resource as T;
        }

        if (typeof(T) == typeof(Sprite))                     //���� ��������Ʈ��� Ű�� �ٲ㼭 ���������ϱ� ��������Ʈ���� Ȯ���� �� 
        {
            _key = _key + ".sprite";
            if (dictionary_Resources.TryGetValue(_key, out UnityEngine.Object sprite))
            {
                return sprite as T;
            }
        }
        return null;
    }

    private void LoadAsync<T>(string _key, Action<T> _callback = null) where T : UnityEngine.Object
    {
        //���ٸ� Ű�� ��巹���� loadasync�� �ϴµ�, ���� Ű�� .sprite�� �ִٸ� ��������Ʈ�� ��
        string loadkey = _key;
        if (_key.Contains(".sprite"))
        {
            loadkey = $"{_key}{_key.Replace(".sprite","")}";
        }

        //�������� key�� ������ �ε� �� �ε� �� ���� ��ųʸ��� �ְ� �ݹ�
        var asyncOperation = Addressables.LoadAssetAsync<T>(loadkey);
        asyncOperation.Completed += (op) => 
        {
            if(!dictionary_Resources.ContainsKey(_key))
                dictionary_Resources.Add(_key, op.Result);
            _callback?.Invoke(op.Result as T);
        };
    }


    public GameObject Instantiate(string _key, Transform _parent = null)
    {
        GameObject po = Managers.Pool.Get(_key);
        if (po != null)
        {
            po.transform.SetParent(_parent);
            return po;
        }

        //�ƴ� ��� �ε�Ǿ� �ִ��� üũ �� �Ǿ� �ִٸ� �̾Ƽ� �ν��Ͻ� �� ����
        //�ε尡 �Ǿ� ���� �ʴٸ� ���� �ε� �� 
        GameObject prefab = Load<GameObject>($"{_key}");
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

        UnityEngine.Object.Destroy(_go);
    }
}
