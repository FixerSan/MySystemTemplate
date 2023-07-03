using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    public WorldData worldData;
    private void OnEnable()
    {
        //SetWorldData();
    }

    public StageData GetStageData(StageThema thema, int stageIndex)
    {
        Stage _stage;

        switch(thema)
        {
            case StageThema.Ice:
                _stage = worldData.ice;
                break;

            case StageThema.Fire:
                _stage = worldData.fire;
                break;

            default:
                return null;
        }

        foreach (var stage in _stage.stages)
        {
            if(stage.stageIndex == stageIndex)
            {
                return stage;
            }
        }

        return null;
    }

    public void SetWorldData()
    {
        worldData = Manager.Instance.database.GetWorldData();
    }

    public void SetWorldData(WorldData _worldData)
    {
        worldData = _worldData;
    }

    public bool CheckClearStage(StageThema thema, string sceneName)
    {
        Stage _stage;

        switch (thema)
        {
            case StageThema.Ice:
                _stage = worldData.ice;
                break;

            case StageThema.Fire:
                _stage = worldData.fire;
                break;

            default:
                return true;
        }

        for (int i = 0; i < _stage.stages.Length; i++)
        {
            if (_stage.stages[i].stageName == sceneName)
                return _stage.stages[i].isClear;
        }

        return true;
    }

    public void ClearStage(StageThema thema, int stageIndex)
    {
        GetStageData(thema, stageIndex).isClear = true;
        Manager.Instance.scene.LoadScene("WorldScene");
        Presenter.Instance.SetCloseAllPanel();
    }
}
