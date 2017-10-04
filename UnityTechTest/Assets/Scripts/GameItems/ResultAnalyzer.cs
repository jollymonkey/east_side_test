using UnityEngine;
using System.Collections;

public enum Result
{
	Won,
	Lost,
	Draw
}

public class ResultAnalyzer
{
	public static Result GetResultState(UseableItemManager useableItemManager, EUseableItem playerHand, EUseableItem enemyHand)
	{
	    if (playerHand == enemyHand)
	    {
	        return Result.Draw;
	    }
	    return isStronger(useableItemManager, playerHand, enemyHand) ? Result.Won : Result.Lost;
	}

	private static bool isStronger (UseableItemManager useableItemManager, EUseableItem firstHand, EUseableItem secondHand)
	{
        EUseableItem[] beats = useableItemManager.GetUseableItemByEnum(firstHand).beats;
	    for (int i = 0; i < beats.Length; i++)
	    {
	        if (beats[i] == secondHand)
	        {
	            return true;
	        }
	    }
	    return false;
	}
}