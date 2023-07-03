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


    //��� ����� ��Ȱ��ȭ �� �Ű����� �̸��� ���� ����� �г� Ȱ��ȭ
    public void SetActiveWorldMapPanel(string worldMapPanelName)
    {
        Presenter.Instance.SetCloseAllPanel();
        Presenter.Instance.panels["WorldMapPanel"].gameObject.SetActive(true);
        Presenter.Instance.panels[worldMapPanelName].gameObject.SetActive(true);
        Presenter.Instance.panels[worldMapPanelName].Effect();

    }

    //��� ����� �г� ��Ȱ��ȭ
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

    //����� Ȱ��ȭ ��
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
