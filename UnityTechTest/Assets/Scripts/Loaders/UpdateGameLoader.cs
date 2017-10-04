using UnityEngine;
using System.Collections;
using System;

public class UpdateGameLoader
{
	public delegate void OnLoadedAction(Hashtable gameUpdateData);
	public event OnLoadedAction OnLoaded;

	private EUseableItem _choice;
    private UseableItemManager _itemManager;
    
	public UpdateGameLoader(UseableItemManager itemManager, EUseableItem playerChoice)
	{
		_choice = playerChoice;
	    _itemManager = itemManager;
	}

	public void load()
	{
		EUseableItem opponentHand = (EUseableItem)Enum.GetValues(typeof(EUseableItem)).GetValue(UnityEngine.Random.Range(1, (int)EUseableItem.MAX));

		Hashtable mockGameUpdate = new Hashtable();
		mockGameUpdate["resultPlayer"] = _choice;
		mockGameUpdate["resultOpponent"] = opponentHand;

        Result result = ResultAnalyzer.GetResultState(_itemManager, _choice, opponentHand);
	    mockGameUpdate["result"] = result;
        mockGameUpdate["coinsAmountChange"] = GetCoinsAmount(result);

        OnLoaded(mockGameUpdate);
	}

	private int GetCoinsAmount (Result drawResult)
	{
		if (drawResult.Equals (Result.Won))
		{
			return 10;
		}
		else if (drawResult.Equals (Result.Lost))
		{
			return -10;
		}
		
		return 0;
	}
}