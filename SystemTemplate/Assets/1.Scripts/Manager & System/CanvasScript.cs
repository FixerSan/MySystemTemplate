using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    #region Singleton, DontDestoryOnLoad, Awake
    private static CanvasScript instance;
    public static CanvasScript Instance
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
}