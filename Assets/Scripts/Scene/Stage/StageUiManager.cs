using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUiManager : MonoBehaviour
{
    public enum ShowType
    {
        Lobby,
        Ready,
        Playing,
    }

    public StageUiTop uiTop;
    public StageUiLobby uiLobby;
    public StageUiReady uiReady;
    public StageUiPlaying uiPlaying;

    private Action _startAction;
    
    public void Init(Action startAction)
    {
        _startAction = startAction;

        uiReady.Init(StartAction);
    }

    private void StartAction()
    {
        ChangeShowType(ShowType.Playing);

        _startAction?.Invoke();
    }

    public void ChangeShowType(ShowType showType)
    {
        if (showType == ShowType.Playing)
            uiReady.Off();
        else
            uiReady.On();
    }
}