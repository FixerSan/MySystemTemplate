using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildScene : BaseScene
{
    public override void Init()
    {
        //Managers.Data.LoadSceneData(Define.Scene.Guild);
        Managers.Object.Spawn<PlayerController>(Vector3.zero);
    }

    public override void Clear()
    {

    }
}
