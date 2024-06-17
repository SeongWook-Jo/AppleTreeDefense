using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InfoManager
{
    public static Dictionary<int, TreeInfo> TreeInfos;
    public static TreeStatInfo TreeStatInfo;

    public static void Init()
    {
        TreeInfos = GetTreeData("TreeData");

        TreeStatInfo = GetTreeStatData("TreeStatData");
    }

    private static Dictionary<int, TreeInfo> GetTreeData(string fileName)
    {
        var info = new Dictionary<int, TreeInfo>();

        var list = CSVReader.Read(GetDataPath(fileName));

        foreach (var item in list)
        {
            var key = item["Id"];

            var value = new TreeInfo((int)item["Id"], (string)item["Name"]);

            info.Add((int)key, value);
        }

        return info;
    }

    private static TreeStatInfo GetTreeStatData(string fileName)
    {
        var info = new TreeStatInfo();

        var list = CSVReader.Read(GetDataPath(fileName));

        foreach (var item in list)
        {
            var statData = new TreeStatInfo.StatData(float.Parse(item["BaseValue"].ToString()), float.Parse(item["UpgradeFactor"].ToString()), (int)item["MaxLevel"], float.Parse(item["BaseCost"].ToString()), float.Parse(item["CostFactor"].ToString()));

            if (Enum.TryParse<TreeStatType>((string)item["StatType"], out var result))
            {
                info.UpdateStatData(result, statData);
            }
            else
            {
                Debug.LogError("TreeStatInfo Init Failed");
            }
        }

        return info;
    }

    private static string GetDataPath(string fileName)
    {
        string preFileName = "GameData";

        return Path.Combine(preFileName, fileName);
    }
}
