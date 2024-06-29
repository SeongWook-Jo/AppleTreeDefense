using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class StageUiLobby : MonoBehaviour
{
    public GameObject treeObj;
    public GameObject houseObj;

    public Action _clickBackBtnAction;

    public StageUiLobbyUpgradeObj[] treeUpgradeObjs;
    public StageUiLobbyUpgradeObj houseUpgradeObj;

    private TreeInstance _selectedTree;

    public void Init()
    {
        treeUpgradeObjs[0].Init(ClickTreeAppleCountUpgradeAction);
        treeUpgradeObjs[1].Init(ClickTreeGrowSpeedUpgradeAction);
        houseUpgradeObj.Init(ClickHouseHealthUpgradeAction);
    }

    public void ClickBackBtn()
    {
        _clickBackBtnAction?.Invoke();
    }

    public void SetClickBackBtnAction(Action action)
    {
        _clickBackBtnAction = action;
    }

    public void ShowTreeUpgrade(TreeInstance tree)
    {
        houseObj.Off();

        treeObj.On();

        _selectedTree = tree;

        RefreshTreeUpgradeObjs(tree);
    }

    private void RefreshTreeUpgradeObjs(TreeInstance tree)
    {
        var currGold = Player.Instance.Gold;

        //applecount
        var price = tree.GetAppleCountUpgradePrice();
        treeUpgradeObjs[0].Set(price, currGold < price);

        //growspeed
        price = tree.GetGrowSpeedUpgradePrice();
        treeUpgradeObjs[1].Set(price, currGold < price);
    }

    private void ClickTreeAppleCountUpgradeAction()
    {
        var currGold = Player.Instance.Gold;

        var tree = _selectedTree;

        var price = tree.GetAppleCountUpgradePrice();

        if (tree.AppleCountLevel >= tree.StatInfo.AppleCountMaxLevel)
        {
            Debug.LogError("Max Level");

            return;
        }

        if (currGold < price)
        {
            Debug.LogError("Lack Gold");

            return;
        }

        Player.Instance.AddGold(-price);

        tree.UpgradeAppleCount();

        RefreshTreeUpgradeObjs(tree);
    }

    private void ClickTreeGrowSpeedUpgradeAction()
    {
        var currGold = Player.Instance.Gold;

        var tree = _selectedTree;

        var price = tree.GetGrowSpeedUpgradePrice();

        if (tree.GrowSpeedLevel >= tree.StatInfo.GrowSpeedMaxLevel)
        {
            Debug.LogError("Max Level");

            return;
        }

        if (currGold < price)
        {
            Debug.LogError("Lack Gold");

            return;
        }

        Player.Instance.AddGold(-price);

        tree.UpgradeGrowSpeed();

        RefreshTreeUpgradeObjs(tree);
    }

    public void ShowHouseUpgrade()
    {
        treeObj.Off();

        houseObj.On();

        RefreshHouseUpgradeObj();
    }

    public void RefreshHouseUpgradeObj()
    {
        var house = Player.Instance.House;

        var price = house.GetHealthUpgradePrice();

        var currGold = Player.Instance.Gold;

        houseUpgradeObj.Set(price, currGold < price);
    }

    private void ClickHouseHealthUpgradeAction()
    {
        var currGold = Player.Instance.Gold;

        var house = Player.Instance.House;

        var houseInfo = InfoManager.HouseInfo;

        var price = house.GetHealthUpgradePrice();

        if (house.HealthLevel >= houseInfo.HealthMaxLevel)
        {
            Debug.LogError("Max Level");

            return;
        }

        if (currGold < price)
        {
            Debug.LogError("Lack Gold");

            return;
        }

        Player.Instance.AddGold(-price);

        house.UpgradeHealth();

        RefreshHouseUpgradeObj();
    }
}
