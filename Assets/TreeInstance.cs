using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInstance
{
    public TreeInstance(int id, bool isOpen)
    {
        Id = id;
        IsOpen = isOpen;
        StatLevels = new Dictionary<TreeStatType, int>();
    }

    public int Id { get; private set; }
    
    public bool IsOpen { get; private set; }

    public Dictionary<TreeStatType, int> StatLevels;

    public int GetAppleCount()
    {
        var statData = InfoManager.TreeStatInfo.StatDatas[TreeStatType.AppleCount];

        if (StatLevels.ContainsKey(TreeStatType.AppleCount))
        {
            var totalFactor = (StatLevels[TreeStatType.AppleCount] - 1) * statData.UpgradeFactor;

            return (int)((statData.BaseValue + totalFactor) / 100);
        }
        else
        {
            return (int)(statData.BaseValue / 100);
        }
    }

    public float GetGrowSpeed()
    {
        var statData = InfoManager.TreeStatInfo.StatDatas[TreeStatType.GrowSpeed];

        if (StatLevels.ContainsKey(TreeStatType.GrowSpeed))
        {
            var totalFactor = (StatLevels[TreeStatType.GrowSpeed] - 1) * statData.UpgradeFactor;

            return statData.BaseValue - totalFactor;
        }
        else
        {
            return statData.BaseValue;
        }
    }
}
