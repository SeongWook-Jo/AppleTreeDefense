using System;
[Serializable]
public class HouseInstance
{
    public int HealthLevel;

    public void SetHealthLevel(int level)
    {
        HealthLevel = level;
    }

    public float GetHealth()
    {
        var info = InfoManager.HouseInfo;
        
        var health = info.HealthBaseValue;

        health += (HealthLevel - 1) * info.HealthUpgradeFactor;
        
        return health;
    }

    public int GetHealthUpgradePrice()
    {
        var info = InfoManager.HouseInfo;

        return info.HealthBaseCost + ((HealthLevel - 1) * info.HealthCostFactor);
    }

    public void UpgradeHealth()
    {
        HealthLevel++;

        Player.Instance.Save();
    }
}
