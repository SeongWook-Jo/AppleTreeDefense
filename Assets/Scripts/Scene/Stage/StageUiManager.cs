using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageUiManager : MonoBehaviour
{
    public enum ShowType
    {
        Lobby,
        Stage,
    }

    public enum UpgradeType
    {
        Tree,
        House,
    }

    public StageUiLobby uiLobby;
    public StageUiStage uiStage;

    private StageManager _manager;

    public void Init(StageManager manager, Action startAction)
    {
        _manager = manager;

        uiStage.Init();

        uiStage.SetStartAction(startAction);

        uiLobby.Init();

        uiLobby.SetClickBackBtnAction(() => ChangeShowType(ShowType.Stage));
    }

    public void GameEnd()
    {
        uiStage.ShowStartBtn(true);

        uiStage.ShowInGameGold(false);
        
        uiStage.ShowWaveText(false);
    }

    public void SetStage(int stageId)
    {
        uiStage.SetStage(stageId);
    }

    public void SetWave(int waveIdx)
    {
        uiStage.SetWave(waveIdx + 1);
    }

    public void ChangeShowType(ShowType showType)
    {
        if (showType == ShowType.Stage)
        {
            uiStage.On();
            uiLobby.Off();
        }
        else
        {
            uiStage.Off();
            uiLobby.On();
        }
    }

    public void ShowInGameGoldAnimation(Vector3 originPos, Vector3 secondPos, int targetGold)
    {
        uiStage.ShowInGameGoldAnimation(originPos, secondPos, targetGold);
    }

    private void OnEnable()
    {
        RefreshGold(Player.Instance.Gold);
    }

    public void RefreshGold(int gold)
    {
        uiStage.SetGold(gold);
    }

    public void ShowUpgradeHouse()
    {
        ChangeShowType(ShowType.Lobby);

        uiLobby.ShowHouseUpgrade();
    }

    public void ShowUpgradeTree(int gardenId)
    {
        if (Player.Instance.TreeList.ContainsKey(gardenId) == false)
        {
            Debug.LogError("TryButGarden");

            return;
        }

        ChangeShowType(ShowType.Lobby);

        var tree = Player.Instance.TreeList[gardenId];

        uiLobby.ShowTreeUpgrade(tree);
    }
}
