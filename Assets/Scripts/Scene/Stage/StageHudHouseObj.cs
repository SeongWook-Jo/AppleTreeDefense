using UnityEngine;
using UnityEngine.UI;

public class StageHudHouseObj : MonoBehaviour
{
    public Image front;

    private House _house;

    public void Init(House house)
    {
        _house = house;
    }

    public void UpdateProgress()
    {
        SetProgress(_house.GetHealthProgress());
    }

    private void SetProgress(float progress)
    {
        front.fillAmount = progress;
    }
}
