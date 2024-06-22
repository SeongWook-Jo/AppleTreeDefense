using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageTreeHudObj : MonoBehaviour
{
    public Image front;

    private Tree _tree;

    public void SetTree(Camera camera, Tree tree)
    {
        _tree = tree;

        var ScreenPos = camera.WorldToScreenPoint(tree.transform.position);

        transform.position = ScreenPos;
    }

    public void UpdateProgree()
    {
        SetProgress(_tree.GetCreateTime());
    }

    private void SetProgress(float progress)
    {
        front.fillAmount = progress;
    }
}
