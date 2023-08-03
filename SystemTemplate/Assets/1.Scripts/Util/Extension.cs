using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    #region GameObject
    public static T GetOrAddComponent<T>(this GameObject _go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(_go);
    }

    public static void BindEvent(this GameObject _go,Action _eventCallback = null, Action<BaseEventData> _dragEventCallback = null, Define.UIEvent _eventType = Define.UIEvent.Click)
    {
        UIBase.BindEvent(_go, _eventCallback, _dragEventCallback, _eventType);
    }

    #endregion
}
