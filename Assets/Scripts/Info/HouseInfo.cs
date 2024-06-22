public class HouseInfo
{
    public HouseInfo(float healthBaseValue, float healthUpgradeFactor, int healthMaxLevel, int healthBaseCost, int healthCostFactor)
    {
        HealthBaseValue = healthBaseValue;
        HealthUpgradeFactor = healthUpgradeFactor;
        HealthMaxLevel = healthMaxLevel;
        HealthBaseCost = healthBaseCost;
        HealthCostFactor = healthCostFactor;
    }
    
    public float HealthBaseValue { get; private set; }
    public float HealthUpgradeFactor { get; private set; }
    public int HealthMaxLevel { get; private set; }
    public int HealthBaseCost { get; private set; }
    public int HealthCostFactor { get; private set; }
}
