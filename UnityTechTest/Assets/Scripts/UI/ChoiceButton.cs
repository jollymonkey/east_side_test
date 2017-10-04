using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ChoiceButton : MonoBehaviour
{
    public delegate void OnClickedAction(EUseableItem useableItem);
    public event OnClickedAction OnClicked;

    [SerializeField] private Button button;
    [SerializeField] private Text label;

    private UseableItem _useableItem;

    public void Initialize(UseableItem useableItem)
    {
        _useableItem = useableItem;
        button.onClick.AddListener(OnClickHandler);

        label.text = useableItem.name;
    }

    private void OnClickHandler()
    {
        OnClicked(_useableItem.value);
    }

}