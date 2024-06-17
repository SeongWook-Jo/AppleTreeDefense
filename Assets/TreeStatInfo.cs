using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeStatInfo
{
    public class StatData
    {
        public StatData(float baseValue, float upgradeFactor, int maxLevel, float baseCost, float costFactor)
        {
            BaseValue = baseValue;
            UpgradeFactor = upgradeFactor;
            MaxLevel = maxLevel;
            BaseCost = baseCost;
            CostFactor = costFactor;
        }

        public float BaseValue { get; private set; }
        public float UpgradeFactor { get; private set; }
        public int MaxLevel { get; private set; }
        public float BaseCost { get; private set; }
        public float CostFactor { get; private set; }
    }

    public TreeStatInfo()
    {
        StatDatas = new Dictionary<TreeStatType, StatData>();
    }

    public Dictionary<TreeStatType, StatData> StatDatas;

    public void UpdateStatData(TreeStatType statType, StatData data)
    {
        if (StatDatas.ContainsKey(statType))
            StatDatas[statType] = data;
        else
            StatDatas.Add(statType, data);
    }
}
