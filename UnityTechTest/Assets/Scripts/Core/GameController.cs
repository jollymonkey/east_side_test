﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
	public Text playerHand;
	public Text enemyHand;

	[SerializeField] private Text _nameLabel;
	[SerializeField] private Text _moneyLabel;

	private Player _player;

    private UseableItemManager _useableItemManager;

    private PlayerInfoLoader _playerInfoLoader;
    private UsableItemsLoader _usableItemsLoader;

	void Start()
	{
        _usableItemsLoader = new UsableItemsLoader();
	    _usableItemsLoader.OnLoaded += OnUseableItemsLoaded;
        _usableItemsLoader.load();
	}

    private void OnUseableItemsLoaded(UseableItem[] possibleItem)
    {
        _useableItemManager = new UseableItemManager();
        _useableItemManager.Initialize(possibleItem);

        _playerInfoLoader = new PlayerInfoLoader();
        _playerInfoLoader.OnLoaded += OnPlayerInfoLoaded;
        _playerInfoLoader.load("testuser"); // todo change to take in name, or pull last user
    }

    private void OnPlayerInfoLoaded(Player playerData)
	{
	    _player = playerData;
        UpdateHud();
	}

	public void UpdateHud()
	{
		_nameLabel.text = "Name: " + _player.GetName();
	    OnMoneyChanged();
	}

    private void OnMoneyChanged()
    {
        _moneyLabel.text = "Money: $" + _player.GetCoins().ToString();
    }

	public void HandlePlayerInput(int item)
	{
		EUseableItem playerChoice = EUseableItem.None;

		switch (item)
		{
			case 1:
				playerChoice = EUseableItem.Rock;
				break;
			case 2:
				playerChoice = EUseableItem.Paper;
				break;
			case 3:
				playerChoice = EUseableItem.Scissors;
				break;
		}

		UpdateGame(playerChoice);
	}

	private void UpdateGame(EUseableItem playerChoice)
	{
		UpdateGameLoader updateGameLoader = new UpdateGameLoader(_useableItemManager, playerChoice);
		updateGameLoader.OnLoaded += OnGameUpdated;
		updateGameLoader.load();
	}

	public void OnGameUpdated(Hashtable gameUpdateData)
	{
		playerHand.text = DisplayResultAsText((EUseableItem)gameUpdateData["resultPlayer"]);
		enemyHand.text = DisplayResultAsText((EUseableItem)gameUpdateData["resultOpponent"]);

		_player.ChangeCoinAmount((int)gameUpdateData["coinsAmountChange"]);
	    OnMoneyChanged();

        _playerInfoLoader.Save();
	}

	private string DisplayResultAsText (EUseableItem result)
	{
	    return _useableItemManager.GetUsableItemNameByEnum(result);
	}
}