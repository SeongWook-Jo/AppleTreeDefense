using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUiStage : MonoBehaviour
{
    public GameObject inGameGoldObj;

    public TextMeshProUGUI stageText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI inGameGold;
    public TextMeshProUGUI waveText;

    public Button startBtn;

    private Action _startAction;

    private int _showGold;

    public void Init()
    {
        startBtn.onClick.AddListener(ClickStartBtn);

        ShowInGameGold(false);

        ShowWaveText(false);
    }

    public void SetStartAction(Action startAction)
    {
        _startAction = startAction;
    }

    public void SetStage(int stageId)
    {
        stageText.text = $"Stage {stageId}";
    }

    public void SetWave(int wave)
    {
        waveText.text = $"Wave {wave}";

        DOTween.To(() => new Color32(255, 238, 49, 255), c => waveText.color = c, new Color32(255, 238, 49, 123), 1.5f).OnComplete(() => waveText.color = new Color32(255, 238, 49, 0));
    }

    public void ShowStartBtn(bool isShow)
    {
        if (isShow)
            startBtn.On();
        else
            startBtn.Off();
    }

    public void ShowInGameGold(bool isShow)
    {
        if (isShow)
            inGameGoldObj.On();
        else
            inGameGoldObj.Off();
    }

    public void ShowWaveText(bool isShow)
    {
        if (isShow)
            waveText.On();
        else
            waveText.Off();
    }

    private void ClickStartBtn()
    {
        RefreshInGameGold(0);

        ShowWaveText(true);

        ShowInGameGold(true);

        ShowStartBtn(false);

        _startAction?.Invoke();
    }

    public void ShowInGameGoldAnimation(Vector3 originPos, Vector3 secondPos, int targetGold)
    {
        var goldObj = ResourceManager.GetGameObjPref("GoldObj").MakeInstance(transform);

        goldObj.transform.position = originPos;

        goldObj.transform.DOMove(secondPos, 0.5f)
            .OnComplete(() => goldObj.transform.DOMove(inGameGoldObj.transform.position, 1f)
            .OnComplete(() =>
            {
                Destroy(goldObj);
                DOTween.To(() => _showGold, g => RefreshInGameGold(g), targetGold, 1f);
            }));
    }

    private void RefreshInGameGold(int gold)
    {
        _showGold = gold;
        inGameGold.text = gold.ToString();
    }
}
