using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUiLobby : MonoBehaviour
{
    public GameObject treeObj;
    public GameObject houseObj;

    public Action _clickBackBtnAction;

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
    }

    public void ShowHouseUpgrade()
    {
        treeObj.Off();

        houseObj.On();
    }
}
