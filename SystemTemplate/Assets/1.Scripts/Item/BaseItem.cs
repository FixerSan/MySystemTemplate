using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem
{
    public ItemData itemData;
    public int itemCount;

    public void UseItem()
    {
        switch(Util.ParseEnum<Define.Item>(itemData.name))
        {
            case Define.Item.TestItem:
                Debug.Log("�׽�Ʈ ������ ���");
                break;
        }
    }

    public BaseItem(ItemData _itemData, int _itemCount) 
    {
        itemData = _itemData;
        itemCount = _itemCount;
    }
}
