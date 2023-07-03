using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    private void OnEnable()
    {
        Setup();
    }

    private void Setup()
    {

    }

    private void Update()
    {
        CheckInput();

    }

    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Presenter.Instance.worldMapPresenter.DrawWorldMap(StageThema.Ice);  //�⺻�� �ӽ÷� ICE�� �س���
            Debug.Log("�� �� ����");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            bool visible = !Presenter.Instance.panels["ClearPanel"].gameObject.activeSelf;
            Presenter.Instance.etcPresenter.SetActiveClearPanel(visible);
        }
    }
}
