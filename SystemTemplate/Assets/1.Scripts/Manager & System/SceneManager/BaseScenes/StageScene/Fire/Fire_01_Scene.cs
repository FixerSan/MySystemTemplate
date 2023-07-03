using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_01_Scen : BaseScene
{
    public override void Clear()
    {
    }

    public override void Setup()
    {
        sceneName = Manager.Instance.scene.GetCurrentSceneName();
        thema = StageThema.Fire;
        stageIndex = 1;
    }
}
