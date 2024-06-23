using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUiReady : MonoBehaviour
{
    public Button startBtn;

    private Action _startAction;

    public void Init(Action startAction)
    {
        _startAction = startAction;
        startBtn.onClick.AddListener(StartAction);
    }

    private void StartAction()
    {
        _startAction?.Invoke();
    }
}
