using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class DataManager
{
    private bool isPreload = false;

    private Dictionary<int, ItemData> itemDataDictionary = new Dictionary<int, ItemData>();
    private Dictionary<int, DialogData> preDialogData = new Dictionary<int, DialogData>();
    private Dictionary<int, DialogData> sceneDialogData = new Dictionary<int, DialogData>();


    public void GetItem(int _itemUID, Action<ItemData> _callback)
    {
        if (itemDataDictionary.TryGetValue(_itemUID, out ItemData itemData))
            _callback?.Invoke(itemData);
    }

    public void GetDialog(int _dialogUID, Action<DialogData> _callback)
    {
        if (sceneDialogData.TryGetValue(_dialogUID, out DialogData dialogData))
            _callback?.Invoke(dialogData);
    }

    public void LoadSceneData(Define.Scene _scene)
    {
        ClearSceneData();
        Managers.Resource.Load<TextAsset>(_scene.ToString()+"Scene",(sceneDataJson) => 
        {
            SceneData sceneData = JsonUtility.FromJson<SceneData>(sceneDataJson.text);
            foreach (var dialog in sceneData.dialogDatas)
            {
                sceneDialogData.TryAdd(dialog.dialogUID, dialog);
            }
        });
    }

    public void ClearSceneData()
    {
        sceneDialogData.Clear();
    }

    public void PreDataLoad()
    {
        if (isPreload)
            return;

        isPreload = true;

        Managers.Resource.Load<TextAsset>("Preload", (preDataJson) =>
        {
            PreData preData = JsonUtility.FromJson<PreData>(preDataJson.text);
            foreach (var item in preData.itemDatas)
            {
                itemDataDictionary.TryAdd(item.itemUID, item);
            }
        });
    }
}

[System.Serializable]
public class SceneData
{
    public DialogData[] dialogDatas;
}

public class PreData
{
    public ItemData[] itemDatas;
    public DialogData[] dialogDatas;
}

[System.Serializable]
public class ItemData
{
    public int itemUID;
    public string name;
    public string description;
    public string itemImageKey;
}

[System.Serializable]
public class DialogData
{
    public int dialogUID;
    public string speakerName;
    public string speakerImageKey;
    public string speakerType;
    public string sentence;
    public string buttonOneContent;
    public string buttonTwoContent;
    public string buttonThreeContent;
    public int nextDialogUID;
}
