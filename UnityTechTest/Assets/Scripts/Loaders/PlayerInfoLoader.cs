using UnityEngine;
using System.Collections;
using System;

public class PlayerInfoLoader
{
    private const string USER_COUNT = "UserCount";

    public delegate void OnLoadedAction(Player playerData);
	public event OnLoadedAction OnLoaded;

    private Player _player;

	public void load(string name)
	{
	    _player = null;

        // if we have previously saved the user data load it and return that
        if (PlayerPrefs.HasKey(name))
	    {
	        string savedData = PlayerPrefs.GetString(name);
            _player = JsonUtility.FromJson<Player>(savedData);
	    }
        // otherwise new user
	    else
	    {
            Hashtable mockPlayerData = new Hashtable();
	        mockPlayerData["userId"] = GetNextUserID();
            mockPlayerData["name"] = name;
            mockPlayerData["coins"] = 50;

            _player = new Player(mockPlayerData);
	    }
        
		OnLoaded(_player);
	}

    private int GetNextUserID()
    {
        int userCount = 0;
        if (PlayerPrefs.HasKey(USER_COUNT))
        {
            userCount = PlayerPrefs.GetInt(USER_COUNT);
        }
        userCount++;
        PlayerPrefs.SetInt(USER_COUNT, userCount);
        PlayerPrefs.Save();
        return userCount;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(_player);

        PlayerPrefs.SetString(_player.GetName(), json);
        PlayerPrefs.Save();
    }
}