using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapPanel : UIPanel
{
    public StageThema stageThema;
    private WorldMapButton[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<WorldMapButton>();
    }

    public override void Effect()
    {
        SetButton();
    }

    public void SetButton()
    {
        foreach (var item in buttons)
        {
            item.CheckCanInteraction();
        }
    }

    private void OnEnable()
    {

    }
}
