using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class InfoManager
{
    public static Dictionary<int, TreeInfo> TreeInfos;

    public static void Init()
    {
        TreeInfos = GetTreeData("TreeData");

        Debug.LogError($"TreeInfo 1 name {TreeInfos[1].Name}");
    }

    private static Dictionary<int, TreeInfo> GetTreeData(string fileName)
    {
        var info = new Dictionary<int, TreeInfo>();

        string preFileName = "GameData";

        var list = CSVReader.Read(Path.Combine(preFileName, fileName));

        foreach (var item in list)
        {
            var key = item["Id"];

            var value = new TreeInfo((int)item["Id"], (string)item["Name"]);

            info.Add((int)key, value);
        }

        return info;
    }
}
