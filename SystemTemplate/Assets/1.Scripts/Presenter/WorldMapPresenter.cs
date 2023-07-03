using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapPresenter : MonoBehaviour
{
    private void OnEnable()
    {
        Setup();
    }

    private void Setup()
    {
        
    }


    //모든 월드맵 비활성화 후 매개변수 이름과 같은 월드맵 패널 활성화
    public void SetActiveWorldMapPanel(string worldMapPanelName)
    {
        Presenter.Instance.SetCloseAllPanel();
        Presenter.Instance.panels["WorldMapPanel"].gameObject.SetActive(true);
        Presenter.Instance.panels[worldMapPanelName].gameObject.SetActive(true);
        Presenter.Instance.panels[worldMapPanelName].Effect();

    }

    //모든 월드맵 패널 비활성화
    public void SetDisableAllWorldMapPanel()
    {
        foreach (var panel in Presenter.Instance.panels)
        {
            if(panel.Value.panelType == PanelType.WorldMap)
            {
                panel.Value.gameObject.SetActive(false);
            }
        }
    }

    //월드맵 활성화 콜
    public void DrawWorldMap(StageThema thema)
    {
        switch (thema)
        {
            case StageThema.Ice:
                SetActiveWorldMapPanel("IceWorldMapPanel");
                break;

            case StageThema.Fire:
                SetActiveWorldMapPanel("FireWorldMapPanel");
                break;
        }
    }
}
