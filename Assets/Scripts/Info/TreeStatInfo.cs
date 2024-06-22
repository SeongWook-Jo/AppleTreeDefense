using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeStatInfo
{
    public TreeStatInfo(
        float growSpeedBaseValue, float growSpeedUpgradeFactor, int growSpeedMaxLevel,
        float growSpeedBaseCost, float growSpeedCostFactor, float appleCountBaseValue,
        float appleCountUpgradeFactor, int appleCountMaxLevel, float appleCountBaseCost,
        float appleCountCostFactor)
    {
        GrowSpeedBaseValue = growSpeedBaseValue;
        GrowSpeedUpgradeFactor = growSpeedUpgradeFactor;
        GrowSpeedMaxLevel = growSpeedMaxLevel;
        GrowSpeedBaseCost = growSpeedBaseCost;
        GrowSpeedCostFactor = growSpeedCostFactor;
        
        AppleCountBaseValue = appleCountBaseValue;
        AppleCountUpgradeFactor = appleCountUpgradeFactor;
        AppleCountMaxLevel = appleCountMaxLevel;
        AppleCountBaseCost = appleCountBaseCost;
        AppleCountCostFactor = appleCountCostFactor;
    }
    
    public float GrowSpeedBaseValue { get; private set; }
    public float GrowSpeedUpgradeFactor { get; private set; }
    public int GrowSpeedMaxLevel { get; private set; }
    public float GrowSpeedBaseCost { get; private set; }
    public float GrowSpeedCostFactor { get; private set; }
    
    public float AppleCountBaseValue { get; private set; }
    public float AppleCountUpgradeFactor { get; private set; }
    public int AppleCountMaxLevel { get; private set; }
    public float AppleCountBaseCost { get; private set; }
    public float AppleCountCostFactor { get; private set; }
}
