using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClearPlayerPrefs
{

    [MenuItem("EastSide/Delete All PlayerPrefs")]
    static public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

public class MakeScriptableObject
{
    [MenuItem("EastSide/Assets/Create UsableItem")]
    public static void CreateMyAsset()
    {
        UseableItem asset = ScriptableObject.CreateInstance<UseableItem>();

        AssetDatabase.CreateAsset(asset, UseableItem.PATH + "NewScripableObject.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
