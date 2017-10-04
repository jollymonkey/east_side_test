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
	    bool isPlayerStronger = isStronger(useableItemManager, playerHand, enemyHand);
	    bool isEneemyStronger = isStronger(useableItemManager, enemyHand, playerHand);

	    if (isPlayerStronger == isEneemyStronger)
	    {
	        return Result.Draw;
	    }else if (isEneemyStronger)
	    {
	        return Result.Lost;
        }
        return Result.Won;
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