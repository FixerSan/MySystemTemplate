using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : BaseButton
{
    public override void OnClickBtn()
    {
        BaseScene currentScene = Manager.Instance.scene.GetCurrentScene();
        Manager.Instance.world.ClearStage(currentScene.thema, currentScene.stageIndex);
    }
}
