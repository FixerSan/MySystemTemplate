using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public string testKey = "TestAsset";
    public List<GameObject> poolingObject = new List<GameObject>();
    public Transform poolingTransform;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Resource.Load<GameObject>(testKey,(go) => 
            {
                poolingObject.Add(Managers.Resource.Instantiate(testKey));
            });

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            poolingObject.Add(Managers.Resource.Instantiate(testKey));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (var item in poolingObject)
            {
                Managers.Resource.Destroy(item);
            }
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Managers.Resource.Load<GameObject>(testKey, (go) =>
            {
                Managers.Pool.CreatePool(go);
            });
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Managers.Resource.Load<GameObject>(testKey, (go) =>
            {
                Managers.Pool.DeletePool(go.name);
            });
        }
    }
}
