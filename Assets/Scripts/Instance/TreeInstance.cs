using System;
[Serializable]
public class TreeInstance
{
    public TreeInstance(int treeId, int growSpeedLevel, int appleCountLevel)
    {
        TreeId = treeId;
        GrowSpeedLevel = growSpeedLevel;
        AppleCountLevel = appleCountLevel;

        var treeInfo = InfoManager.TreeInfos[TreeId];
        StatInfo = InfoManager.TreeStatInfos[treeInfo.StatId];
    }
    
    public int TreeId { get; private set; }
    public int GrowSpeedLevel { get; private set; }
    public int AppleCountLevel { get; private set; }
    public TreeStatInfo StatInfo { get; private set; } 

    public float GetGrowSpeed()
    {
        float speed = StatInfo.GrowSpeedBaseValue;

        speed -= (GrowSpeedLevel - 1) * StatInfo.GrowSpeedUpgradeFactor;

        return speed;
    }

    public int GetAppleCount()
    {
        float count = StatInfo.AppleCountBaseValue;

        count += (AppleCountLevel - 1) * StatInfo.AppleCountUpgradeFactor;

        return (int)count;
    }

    public int GetAppleCountUpgradePrice()
    {
        var baseCost = StatInfo.AppleCountBaseCost;

        var levelFactor = StatInfo.AppleCountCostFactor;

        return baseCost + ((AppleCountLevel - 1) * levelFactor);
    }

    public int GetGrowSpeedUpgradePrice()
    {
        var baseCost = StatInfo.GrowSpeedBaseCost;

        var levelFactor = StatInfo.GrowSpeedCostFactor;

        return baseCost + ((GrowSpeedLevel - 1) * levelFactor);
    }

    public void UpgradeAppleCount()
    {
        AppleCountLevel++;

        Player.Instance.Save();
    }

    public void UpgradeGrowSpeed()
    {
        GrowSpeedLevel++;

        Player.Instance.Save();
    }
}
