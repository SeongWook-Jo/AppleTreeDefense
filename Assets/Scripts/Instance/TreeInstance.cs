using System;
[Serializable]
public class TreeInstance
{
    public TreeInstance(int treeId, int growSpeedLevel, int appleCountLevel)
    {
        TreeId = treeId;
        GrowSpeedLevel = growSpeedLevel;
        AppleCountLevel = appleCountLevel;
    }
    
    public int TreeId { get; private set; }
    public int GrowSpeedLevel { get; private set; }
    public int AppleCountLevel { get; private set; }

    public float GetGrowSpeed()
    {
        var statInfo = GetStatInfo();

        float speed = statInfo.GrowSpeedBaseValue;

        speed -= (GrowSpeedLevel - 1) * statInfo.GrowSpeedUpgradeFactor;

        return speed;
    }

    public int GetAppleCount()
    {
        var statInfo = GetStatInfo();

        float count = statInfo.AppleCountBaseValue;

        count += (AppleCountLevel - 1) * statInfo.AppleCountUpgradeFactor;

        return (int)count;
    }

    private TreeStatInfo GetStatInfo()
    {
        var treeInfo = InfoManager.TreeInfos[TreeId];

        return InfoManager.TreeStatInfo[treeInfo.StatId];
    }
}
