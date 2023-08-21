using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    private PlayerController player;
    public PlayerController Player 
    {
        get 
        {
            if (player == null)
                player = GameObject.FindObjectOfType<PlayerController>();    
            return player; 
        } 
    }
    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>();
    public Transform MonsterTransform
    {
        get
        {
            GameObject root = GameObject.Find("@Monster");
            if (root == null)
            {
                root = new GameObject { name = "@Monster" };
                root.transform.position = Vector3.zero;
            }

            return root.transform;
        }
    }

    public Inventory Inventory
    {
        get 
        {
            if(player == null)
            {
                player = GameObject.FindObjectOfType<PlayerController>();
                return player.GetOrAddComponent<Inventory>();
            }
            Debug.LogError("플레이어가 스폰되지 않았음");
            return null;
        }
    }

    public ObjectManager()
    {
        Init();
    }

    public void Init()
    {
        //player = Managers.FindObjectOfType<PlayerController>();
    }

    public void Clear()
    {
        Monsters.Clear();
    }

    public T Spawn<T>(Vector3 _position) where T : BaseController
    {
        System.Type type = typeof(T);

        if(type == typeof(PlayerController))
        {
            //여기 써야함
            GameObject go = Managers.Resource.Instantiate("Player");
            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            pc.transform.position = _position;
            player = pc;
            return pc as T;
        }

        if (type == typeof(MonsterController))
        {
            GameObject go = Managers.Resource.Instantiate("Monster", _pooling: true);
            MonsterController mc = go.GetOrAddComponent<MonsterController>();
            go.transform.position = _position;
            Monsters.Add(mc);
            return mc as T;
        }

        return null;
    }

    public void Despawn<T>(T _object) where T : BaseController
    {
        System.Type type = typeof(T);

        if (type == typeof(MonsterController))
        {
            Monsters.Remove(_object as MonsterController);
            Managers.Resource.Destroy(_object.gameObject);
        }
    }

    public void CreateItem<T>(ItemData itemData, int _count = 1) where T : BaseItem
    {
        BaseItem baseItem = new BaseItem(itemData, _count);

    }
}
