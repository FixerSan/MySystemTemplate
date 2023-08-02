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

    //로딩창 사용 용도
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

    //이미 로드 된 것(딕셔너리)을 뽑아올 때
    private T Load<T>(string _key) where T : UnityEngine.Object
    {
        if (dictionary_Resources.TryGetValue(_key, out UnityEngine.Object resource))  //키를 통해서 찾아보고 있다면 나온 반환을 다시 새로 만든 지역 변수 안에 넣는 느낌인 듯?
        {
            return resource as T;
        }

        if (typeof(T) == typeof(Sprite))                     //만약 스프라이트라면 키를 바꿔서 저장했으니까 스프라이트인지 확인한 후 
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
        //없다면 키로 어드레서블 loadasync를 하는데, 만약 키에 .sprite가 있다면 스프라이트를 뺌
        string loadkey = _key;
        if (_key.Contains(".sprite"))
        {
            loadkey = $"{_key}{_key.Replace(".sprite","")}";
        }

        //최종적인 key를 가지고 로드 후 로드 된 값을 딕셔너리에 넣고 콜백
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

        //아닐 경우 로드되어 있는지 체크 후 되어 있다면 뽑아서 인스턴스 후 리턴
        //로드가 되어 있지 않다면 새로 로드 후 
        GameObject prefab = Load<GameObject>($"{_key}");
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

        UnityEngine.Object.Destroy(_go);
    }
}
