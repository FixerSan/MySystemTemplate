using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    public string panelName;
    public PanelType panelType;

    public virtual void Effect()
    {

    }
}

public enum PanelType { Etc, WorldMap }
