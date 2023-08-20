using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildScene : BaseScene
{
    public override void Init()
    {
        Managers.Data.LoadSceneData(Define.Scene.Guild);
    }

    public override void Clear()
    {

    }
}
