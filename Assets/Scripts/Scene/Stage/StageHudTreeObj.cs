using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageHudTreeObj : MonoBehaviour
{
    public Image front;

    private Tree _tree;

    public void SetTree(Camera camera, Tree tree)
    {
        _tree = tree;

        _tree.SetShowHudAction(Show);

        _tree.SetHideHudAction(Hide);

        var ScreenPos = camera.WorldToScreenPoint(tree.transform.position);

        transform.position = ScreenPos;
    }

    public void UpdateProgree()
    {
        SetProgress(_tree.GetCreateProgress());
    }

    private void SetProgress(float progress)
    {
        front.fillAmount = progress;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

}
