using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class UseableItemManager
{
    private Dictionary<EUseableItem, UseableItem> _useablItemObjects;
    public void Initialize(UseableItem[] useableItems)
    {
        _useablItemObjects = new Dictionary<EUseableItem, UseableItem>();
        for (int i = 0; i < useableItems.Length; i++)
        {
            _useablItemObjects.Add(useableItems[i].value, useableItems[i]);
        }
    }

    public string GetUsableItemNameByEnum(EUseableItem item)
    {
        if (_useablItemObjects.ContainsKey(item))
        {
            return _useablItemObjects[item].name;
        }
        return "Nothing";
    }

    public UseableItem GetUseableItemByEnum(EUseableItem item)
    {
        return _useablItemObjects[item];
    }
}