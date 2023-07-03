using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData
{
    public Stage ice;
    public Stage fire;
}

[System.Serializable]
public class Stage
{
    public StageThema thema;
    public StageData[] stages;
}

[System.Serializable]
public class StageData
{
    public int stageIndex;
    public string stageName;
    public bool isClear;
}

public enum StageThema { Ice, Fire }
