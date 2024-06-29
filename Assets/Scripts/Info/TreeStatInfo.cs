using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeStatInfo
{
    public TreeStatInfo(
        float growSpeedBaseValue, float growSpeedUpgradeFactor, int growSpeedMaxLevel,
        int growSpeedBaseCost, int growSpeedCostFactor, float appleCountBaseValue,
        float appleCountUpgradeFactor, int appleCountMaxLevel, int appleCountBaseCost,
        int appleCountCostFactor)
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
    public int GrowSpeedBaseCost { get; private set; }
    public int GrowSpeedCostFactor { get; private set; }
    
    public float AppleCountBaseValue { get; private set; }
    public float AppleCountUpgradeFactor { get; private set; }
    public int AppleCountMaxLevel { get; private set; }
    public int AppleCountBaseCost { get; private set; }
    public int AppleCountCostFactor { get; private set; }
}
