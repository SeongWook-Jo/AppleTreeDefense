using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class StageUiLobbyUpgradeObj : MonoBehaviour
{
    public Button upgradeBtn;

    public Image img;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI priceText;

    public void Init(Action clickAction)
    {
        upgradeBtn.onClick.AddListener(() => clickAction?.Invoke());
    }

    public void Set( int price, bool isLock)
    {
        priceText.text = price.ToString();
    }
}
