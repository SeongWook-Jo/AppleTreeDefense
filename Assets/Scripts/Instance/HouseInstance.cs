using System;
[Serializable]
public class HouseInstance
{
    public int HealthLevel { get; private set; }

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
}
