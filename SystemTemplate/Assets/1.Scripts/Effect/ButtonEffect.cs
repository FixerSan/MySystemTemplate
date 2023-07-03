using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEffect
{
    public void SelectBtn(int btnIndex)
    {
        switch(btnIndex)
        {
            case 0:
                Presenter.Instance.worldMapPresenter.DrawWorldMap(StageThema.Ice);
                break;

            case 1:
                Presenter.Instance.worldMapPresenter.DrawWorldMap(StageThema.Fire);
                break;
        }
    }
}
