using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public string sceneName;
    public StageThema thema;
    public int stageIndex;

    private void Awake()
    {
        Setup();
    }

    private void OnDestroy()
    {
        Clear();
    }

    public abstract void Setup();
    public abstract void Clear();
}
