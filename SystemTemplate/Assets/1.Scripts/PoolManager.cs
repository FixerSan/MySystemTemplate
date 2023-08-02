using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private GameObject prefab;
    private Queue<GameObject> queue_PoolObject;
    private Transform transform_Pool;
    private string poolName;

    public Pool(GameObject _prefab, string _poolName)
    {
        prefab = _prefab;
        poolName = _poolName;
        queue_PoolObject = new Queue<GameObject>();
        Init();
    }

    private void Init()
    {
        GameObject go = GameObject.Find("@Pool");
        if(go == null)
        {
            go = new GameObject{ name = "@Pool" };
        }
        GameObject _transform_Pool = new GameObject { name = poolName };
        _transform_Pool.transform.SetParent(go.transform);
        transform_Pool = _transform_Pool.transform;
    }

    public GameObject Get()
    {
        GameObject poolObject;
        if (queue_PoolObject.TryDequeue(out GameObject _poolObject))
        {
            poolObject = _poolObject;
        }

        else
        {
            poolObject = GameObject.Instantiate(prefab);
            poolObject.name = prefab.name;
        }

        poolObject.SetActive(true);
        return poolObject;
    }

    public void Push(GameObject _poolObject)
    {
        _poolObject.transform.SetParent(transform_Pool);
        _poolObject.SetActive(false);
        queue_PoolObject.Enqueue(_poolObject);
    }

    public void Clear()
    {
        queue_PoolObject.Clear();
        Managers.Resource.Destroy(transform_Pool.gameObject);
    }
}

public class PoolManager
{
    public Dictionary<string, Pool> dictionary_Pool = new Dictionary<string, Pool>();

    public void Clear()
    {
        dictionary_Pool.Clear();
    }

    public GameObject Get(string _key)
    {
        if (dictionary_Pool.TryGetValue(_key, out Pool pool))
        {
            return pool.Get();
        }
        return null;
    }

    public bool Push(GameObject _go)
    {
        if(dictionary_Pool.ContainsKey(_go.name))
        {
            dictionary_Pool[_go.name].Push(_go);
            return true;
        }
        return false;
    }

    public void CreatePool(GameObject _prefab, System.Action _callback = null)
    {
        string key = _prefab.name;
        if (dictionary_Pool.ContainsKey(key))
            return;
        Pool pool = new Pool(_prefab, $"{key} Pool");
        dictionary_Pool.Add(key, pool);
        _callback?.Invoke();
    }

    public void DeletePool(string _key)
    {
        if(dictionary_Pool.ContainsKey(_key))
        {
            dictionary_Pool[_key].Clear();
            dictionary_Pool.Remove(_key);
        }
    }

    public PoolManager()
    {
        
    }
}
