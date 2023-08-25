using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Sound.FadeChangeBGM(Define.AudioClip_BGM.BGM_1,2.5f);
        }
    }
}
