using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapButton : BaseButton
{
    public string sceneName;
    public StageThema thema;

    public override void OnClickBtn()
    {
        Manager.Instance.scene.LoadScene(sceneName);
    }

    public void CheckCanInteraction()
    {
        btn.interactable = !Manager.Instance.world.CheckClearStage(thema,sceneName);
    }
}
