using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class Database : MonoBehaviour
{
    public AllData alldata;

    private TextAsset json;
    private void OnEnable()
    {
        Setup();
    }

    void Setup()
    {
        Addressables.LoadAssetAsync<TextAsset>("Data").Completed += handle =>
        {
            json = handle.Result;
            alldata = JsonUtility.FromJson<AllData>(json.ToString());
            Manager.Instance.world.SetWorldData(alldata.worldDatas);
        };
    }

    public WorldData GetWorldData()
    {
        return alldata.worldDatas;
    }

    [ContextMenu("데이터 저장")]
    public void SaveData()
    {
        string toJson = JsonUtility.ToJson(alldata,true);

        string fileName = "data.json";
        string path = Application.dataPath + "/" + fileName;

        System.IO.File.WriteAllText(path, toJson);
    }
}

[System.Serializable]
public class AllData
{
    public WorldData worldDatas;
}
