using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ETCPresenter : MonoBehaviour
{
    public void SetActiveClearPanel(bool visible)
    {
        Presenter.Instance.panels["ClearPanel"].gameObject.SetActive(visible);
    }
}
