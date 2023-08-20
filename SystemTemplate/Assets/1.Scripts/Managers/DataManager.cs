using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    private bool isPreload = false;

    private Dictionary<int, ItemData> itemDataDictionary = new Dictionary<int, ItemData>();
    private Dictionary<int, DialogData> preDialogData = new Dictionary<int, DialogData>();
    private Dictionary<int, DialogData> sceneDialogData = new Dictionary<int, DialogData>();

    public void LoadSceneData(Define.Scene _scene)
    {
        TextAsset sceneData = null;
        Managers.Resource.Load<TextAsset>(_scene.ToString()+"Scene",(sd) => 
        {
            sceneData = sd;

        });
        //datas = JsonUtility.FromJson<AllData>(data.text);
    }

    public void GetItem(int _itemUID, Action<ItemData> _callback)
    {
        if (itemDataDictionary.TryGetValue(_itemUID, out ItemData itemData))
            _callback(itemData);
    }

    public void PreDataLoad()
    {
        if (isPreload)
            return;

        isPreload = true;

        TextAsset sceneData = null;
        Managers.Resource.Load<TextAsset>("Preload", (sd) =>
        {
            sceneData = sd;
            Debug.Log(sceneData.text);
        });
    }
}

public class AllData
{
    public ItemData[] itemDatas; 
}

public class ItemData
{
    public int itemUID;
    public string name;
    public string description;
    public string imageKey;
}

public class DialogData
{

}
