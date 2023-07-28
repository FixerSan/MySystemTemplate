using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : PoolObject
{
    private T prefab;
    private Queue<T> queue_PoolObject;
    private Transform transform_Pool;

    public Pool(T _prefab)
    {
        prefab = _prefab;
        Init();
    }

    private void Init()
    {
        GameObject go = GameObject.Find("@Pool");
        if(go == null)
        {
            go = new GameObject{ name = "@Pool" };
        }
        GameObject _transform_Pool = new GameObject { name = $"{typeof(T)}" };
        _transform_Pool.transform.SetParent(go.transform);
        transform_Pool = _transform_Pool.transform;
    }

    public T Get()
    {
        if(queue_PoolObject.TryDequeue(out T poolObject))
        {
            poolObject.SetUp();
            return poolObject;
        }

        else
        {
            T _poolObject = GameObject.Instantiate<T>(prefab);
            _poolObject.SetUp();
            return _poolObject;
        }
    }

    public void Push(T _poolObject)
    {
        _poolObject.Clear();
        _poolObject.transform.SetParent(transform_Pool);
        queue_PoolObject.Enqueue(_poolObject);
    }
}

public class PoolManager
{
    public Dictionary<string, Pool<PoolObject>> dictionary_Pool = new Dictionary<string, Pool<PoolObject>>();

    public PoolManager()
    {
        
    }
    public PoolObject Get(string _key)
    {
        if (dictionary_Pool.TryGetValue(_key, out Pool<PoolObject> _pool))
        {
            return _pool.Get();
        }
        return null;
    }

    public void Push()
    {

    }
}
