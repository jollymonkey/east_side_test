using UnityEngine;
using System.Collections;
using System;

public class UsableItemsLoader
{
    public delegate void OnLoadedAction(UseableItem[] possibleItem);
	public event OnLoadedAction OnLoaded;

    private UseableItem[] possibleItem;

	public void load()
	{
        possibleItem = Resources.LoadAll<UseableItem>(UseableItem.PATH.Substring(UseableItem.PATH.IndexOf("Resources/") + 10));
		OnLoaded(possibleItem);
	}
    
}