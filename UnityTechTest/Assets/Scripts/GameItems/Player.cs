using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Player
{
    [SerializeField] private int _userId;
	[SerializeField] private string _name;
	[SerializeField] private int _coins;

	public Player(Hashtable playerData)
	{
		_userId = (int)playerData["userId"];
		_name = playerData["name"].ToString (); 
		_coins = (int)playerData["coins"];
	}
	
	public int GetUserId()
	{
		return _userId;
	}
	
	public string GetName()
	{
		return _name;
	}

	public int GetCoins()
	{
		return _coins;
	}

	public void ChangeCoinAmount(int amount)
	{
		_coins += amount;
	}
}