using UnityEngine;
using System.Collections;
using System;

public class UpdateGameLoader
{
	public delegate void OnLoadedAction(Hashtable gameUpdateData);
	public event OnLoadedAction OnLoaded;

	private EUseableItem _choice;
    private UseableItemManager _itemManager;
    private int _betAmount;
    
	public UpdateGameLoader(UseableItemManager itemManager, EUseableItem playerChoice, int betAmount)
	{
		_choice = playerChoice;
	    _itemManager = itemManager; // only passing this to simulate the server
	    _betAmount = betAmount;
	}

	public void load()
	{
		EUseableItem opponentHand = (EUseableItem)Enum.GetValues(typeof(EUseableItem)).GetValue(UnityEngine.Random.Range(1, (int)EUseableItem.MAX));

		Hashtable mockGameUpdate = new Hashtable();
		mockGameUpdate["resultPlayer"] = _choice;
		mockGameUpdate["resultOpponent"] = opponentHand;

        Result result = ResultAnalyzer.GetResultState(_itemManager, _choice, opponentHand);
	    mockGameUpdate["result"] = result;
	    mockGameUpdate["coinsAmountChange"] = GetCoinsAmount(result, _betAmount);

        OnLoaded(mockGameUpdate);
	}

	private int GetCoinsAmount (Result drawResult, int betAmount)
	{
		if (drawResult.Equals (Result.Won))
		{
			return betAmount;
		}
		else if (drawResult.Equals (Result.Lost))
		{
			return -betAmount;
		}
		
		return 0;
	}
}