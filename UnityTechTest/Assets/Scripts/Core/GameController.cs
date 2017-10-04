using UnityEngine;
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
    [SerializeField] private Text _resultText;

    [SerializeField] private GridLayoutGroup choiceButtonGrid;
    [SerializeField] private ChoiceButton _choiceButtonPrefab;

    private List<ChoiceButton> choiceButons;

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

        choiceButons = new List<ChoiceButton>();
        for (int i = 0; i < possibleItem.Length; i++)
        {
            ChoiceButton c = Instantiate(_choiceButtonPrefab);
            c.Initialize(_useableItemManager.GetUseableItemByEnum(possibleItem[i].value));
            choiceButons.Add(c);
            c.OnClicked += HandlePlayerInput;
            c.transform.parent = choiceButtonGrid.transform;
            c.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        }
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

	public void HandlePlayerInput(EUseableItem playerChoice)
	{
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
		playerHand.text = DisplaySelectionAsText((EUseableItem)gameUpdateData["resultPlayer"]);
		enemyHand.text = DisplaySelectionAsText((EUseableItem)gameUpdateData["resultOpponent"]);

	    Result result = (Result)gameUpdateData["result"];
	    DisplayResultAsText(result);

        _player.ChangeCoinAmount((int)gameUpdateData["coinsAmountChange"]);
	    OnMoneyChanged();

        _playerInfoLoader.Save();
	}

	private string DisplaySelectionAsText (EUseableItem result)
	{
	    return _useableItemManager.GetUsableItemNameByEnum(result);
	}

    private void DisplayResultAsText(Result result)
    {
        if (result == Result.Won)
        {
            _resultText.text = "You Won";
            _resultText.color = Color.green;
        }
        else if (result == Result.Lost)
        {
            _resultText.text = "You Lost";
            _resultText.color = Color.green;
        }
        else
        {
            _resultText.text = "Draw";
            _resultText.color = Color.yellow;
        }
    }
}