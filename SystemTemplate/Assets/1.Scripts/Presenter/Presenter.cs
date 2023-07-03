using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter : MonoBehaviour          //UI 드로우 기능
{
    #region Singleton, DontDestoryOnLoad, Awake
    private static Presenter instance;
    public static Presenter Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            return null;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public WorldMapPresenter worldMapPresenter;     //월드 맵 UI 드로우 기능
    public ETCPresenter etcPresenter;               //기타 UI 드로우 기능
    
    public Dictionary<string, UIPanel> panels = new Dictionary<string, UIPanel>();
    public Canvas canvas;

    private Stack<UIPopup> popupStack = new Stack<UIPopup>();
    private ButtonEffect buttonEffect = new ButtonEffect();

    private void Setup()
    {
        worldMapPresenter = gameObject.AddComponent<WorldMapPresenter>();
        etcPresenter = gameObject.AddComponent<ETCPresenter>();

        UIPanel[] _panels = FindObjectsOfType<UIPanel>(true);
        for (int i = 0; i < _panels.Length; i++)
        {
            panels.Add(_panels[i].panelName, _panels[i]);
        }

        canvas = FindObjectOfType<Canvas>(true);
        canvas.transform.SetParent(null);
        DontDestroyOnLoad(canvas.gameObject);
        SetCloseAllPanel();
    }

    private void OnEnable()
    {
        Setup();
    }

    private void OnDisable()
    {
        Destroy(GetComponent<WorldMapPresenter>());
        Destroy(GetComponent<ETCPresenter>());
    }

    public void SelectBtn(int btnIndex)
    {
        buttonEffect.SelectBtn(btnIndex);
    }

    public void SetCloseAllPanel()
    {
        foreach (var panel in panels)
        {
            panel.Value.gameObject.SetActive(false);
        }
    }
}
