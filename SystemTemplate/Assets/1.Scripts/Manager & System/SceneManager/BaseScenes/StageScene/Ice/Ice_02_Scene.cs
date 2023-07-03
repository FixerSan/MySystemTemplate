using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice_02_Scene : BaseScene
{
    public override void Clear()
    {
    }

    public override void Setup()
    {
        sceneName = Manager.Instance.scene.GetCurrentSceneName();
        thema = StageThema.Ice;
        stageIndex = 2;
    }
}
