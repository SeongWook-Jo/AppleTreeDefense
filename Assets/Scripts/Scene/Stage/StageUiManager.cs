using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageUiManager : MonoBehaviour
{
    public enum ShowType
    {
        Lobby,
        Playing,
    }

    public StageUiLobby uiLobby;
    public StageUiStage uiStage;

    private StageManager _manager;

    public void Init(StageManager manager, Action startAction)
    {
        _manager = manager;

        uiStage.Init();

        uiStage.SetStartAction(startAction);
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
        if (showType == ShowType.Playing)
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
}
