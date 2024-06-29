using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InfoManager
{
    public static HouseInfo HouseInfo;
    public static Dictionary<int, GardenInfo> GardenInfos;
    public static Dictionary<int, TreeInfo> TreeInfos;
    public static Dictionary<int, TreeStatInfo> TreeStatInfos;
    public static Dictionary<int, EnemyInfo> EnemyInfos;
    public static Dictionary<int, StageInfo> StageInfos;
    public static Dictionary<int, WaveInfo> WaveInfos;

    public static void Init()
    {
        HouseInfo = GetHouseData("HouseData");
        GardenInfos = GetGardenData("GardenData");
        TreeInfos = GetTreeData("TreeData");
        TreeStatInfos = GetTreeStatData("TreeStatData");
        EnemyInfos = GetEnemyData("EnemyData");
        StageInfos = GetStageData("StageData");
        WaveInfos = GetWaveData("WaveData");
    }

    private static HouseInfo GetHouseData(string fileName)
    {
        var list = CSVReader.Read(GetDataPath(fileName));

        var healthBaseValue = float.Parse(list[0]["HealthBaseValue"].ToString());
        var healthUpgradeFactor = float.Parse(list[0]["HealthUpgradeFactor"].ToString());
        var healthMaxLevel = int.Parse(list[0]["HealthMaxLevel"].ToString());
        var healthBaseCost = int.Parse(list[0]["HealthBaseCost"].ToString());
        var healthCostFactor = int.Parse(list[0]["HealthCostFactor"].ToString());

        var houseInfo = new HouseInfo(
            healthBaseValue,
            healthUpgradeFactor,
            healthMaxLevel,
            healthBaseCost,
            healthCostFactor
        );

        return houseInfo;
    }

    private static Dictionary<int, GardenInfo> GetGardenData(string fileName)
    {
        var list = CSVReader.Read(GetDataPath(fileName));

        var info = new Dictionary<int, GardenInfo>();

        foreach (var item in list)
        {
            var key = int.Parse(item["Id"].ToString());
            var openStage = int.Parse(item["OpenStage"].ToString());
            var buyGold = int.Parse(item["BuyGold"].ToString());
            var gardenInfo = new GardenInfo(openStage, buyGold);

            info.Add((int)key, gardenInfo);
        }

        return info;
    }
    private static Dictionary<int, TreeInfo> GetTreeData(string fileName)
    {
        var list = CSVReader.Read(GetDataPath(fileName));

        var info = new Dictionary<int, TreeInfo>();

        foreach (var item in list)
        {
            var key = int.Parse(item["Id"].ToString());
            var statId = int.Parse(item["StatId"].ToString());
            var treeInfo = new TreeInfo(statId);

            info.Add(key, treeInfo);
        }

        return info;
    }

    private static Dictionary<int, TreeStatInfo> GetTreeStatData(string fileName)
    {
        var list = CSVReader.Read(GetDataPath(fileName));

        var info = new Dictionary<int, TreeStatInfo>();

        foreach (var item in list)
        {
            var id = int.Parse(item["Id"].ToString());
            var growSpeedBaseValue = float.Parse(item["GrowSpeedBaseValue"].ToString());
            var growSpeedUpgradeFactor = float.Parse(item["GrowSpeedUpgradeFactor"].ToString());
            var growSpeedMaxLevel = int.Parse(item["GrowSpeedMaxLevel"].ToString());
            var growSpeedBaseCost = int.Parse(item["GrowSpeedBaseCost"].ToString());
            var growSpeedCostFactor = int.Parse(item["GrowSpeedCostFactor"].ToString());

            var appleCountBaseValue = float.Parse(item["AppleCountBaseValue"].ToString());
            var appleCountUpgradeFactor = float.Parse(item["AppleCountUpgradeFactor"].ToString());
            var appleCountMaxLevel = int.Parse(item["AppleCountMaxLevel"].ToString());
            var appleCountBaseCost = int.Parse(item["AppleCountBaseCost"].ToString());
            var appleCountCostFactor = int.Parse(item["AppleCountCostFactor"].ToString());

            var treeStatInfo = new TreeStatInfo(
                growSpeedBaseValue, growSpeedUpgradeFactor, growSpeedMaxLevel,
                growSpeedBaseCost, growSpeedCostFactor, appleCountBaseValue,
                appleCountUpgradeFactor, appleCountMaxLevel, appleCountBaseCost,
                appleCountCostFactor
            );

            info.Add(id, treeStatInfo);
        }

        return info;
    }

    private static Dictionary<int, EnemyInfo> GetEnemyData(string fileName)
    {
        var list = CSVReader.Read(GetDataPath(fileName));

        var info = new Dictionary<int, EnemyInfo>();

        foreach (var item in list)
        {
            var id = int.Parse(item["Id"].ToString());
            var health = float.Parse(item["Health"].ToString());
            var speed = float.Parse(item["Speed"].ToString());
            var attackType = (AttackType)Enum.Parse(typeof(AttackType), item["AttackType"].ToString());
            var attackTime = float.Parse(item["AttackTime"].ToString());
            var damage = float.Parse(item["Damage"].ToString());
            var dropGold = int.Parse(item["DropGold"].ToString());

            var enemyInfo = new EnemyInfo(id, health, speed, attackType, attackTime, damage, dropGold);
            info.Add(id, enemyInfo);
        }

        return info;
    }

    private static Dictionary<int, StageInfo> GetStageData(string fileName)
    {
        var list = CSVReader.Read(GetDataPath(fileName));

        var info = new Dictionary<int, StageInfo>();

        foreach (var item in list)
        {
            var id = int.Parse(item["Id"].ToString());
            var waveTerm = float.Parse(item["WaveTerm"].ToString());
            var waves = Array.ConvertAll(item["Waves"].ToString().Split('_'), int.Parse);

            var stageInfo = new StageInfo(id, waveTerm, waves);
            info.Add(id, stageInfo);
        }

        return info;
    }

    private static Dictionary<int, WaveInfo> GetWaveData(string fileName)
    {
        var list = CSVReader.Read(GetDataPath(fileName));

        var info = new Dictionary<int, WaveInfo>();

        foreach (var item in list)
        {
            var id = int.Parse(item["Id"].ToString());
            var enemies = Array.ConvertAll(item["Enemies"].ToString().Split('_'), int.Parse);

            var waveInfo = new WaveInfo(id, enemies);
            info.Add(id, waveInfo);
        }

        return info;
    }

    private static string GetDataPath(string fileName)
    {
        string preFileName = "GameData";

        return Path.Combine(preFileName, fileName);
    }
}
