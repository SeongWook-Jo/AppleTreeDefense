using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[Serializable]
public class Player
{
    #region  Singleton

    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Player();

                _instance.Init();
            }

            return _instance;
        }
    }

    private static Player _instance;

    #endregion

    #region Property

    public int Gold;

    public int LastestClearStage;

    #endregion

    #region Instance
    public HouseInstance House;

    public Dictionary<int, GardenInstance> GardenList;

    public Dictionary<int, TreeInstance> TreeList;

    #endregion

    private Action _loadEndAction;

    public void Init()
    {
        GardenList = new Dictionary<int, GardenInstance>();
        TreeList = new Dictionary<int, TreeInstance>();
        House = new HouseInstance();
    }

    private void CreateNewAccountForTest()
    {
        for (int i = 1; i < 2; i++)
        {
            GardenList.Add(i, new GardenInstance(true));
            TreeList.Add(i, new TreeInstance(1, 1, 1));
        }

        LastestClearStage = 1;

        House.SetHealthLevel(1);

        Gold = 0;

        UnityEngine.Debug.LogError("CreateNewAccountForTest");

        Save();
    }

    public void Save()
    {
        SaveLoadManager.Save(this);
        UnityEngine.Debug.LogError("Save");
    }

    public void Load(Action endAction)
    {
        _loadEndAction = endAction;

        _instance.Init();

        UnityEngine.Debug.LogError("Load");

        SaveLoadManager.Load(LoadComplete, LoadFailed);
    }

    public void LoadComplete(Player player)
    {
        UnityEngine.Debug.LogError("LoadComplete");

        _instance = player;

        _loadEndAction?.Invoke();
    }

    public void LoadFailed()
    {
        UnityEngine.Debug.LogError("LoadFailed");

        CreateNewAccountForTest();

        _loadEndAction?.Invoke();
    }

    public void SetLatestStage(int stage)
    {
        LastestClearStage = stage;
    }

    public void AddGold(int gold)
    {
        Gold += gold;

        Mathf.Clamp(Gold, 0, int.MaxValue);
    }

    public void OpenTree(int gardenId)
    {
        TreeList.Add(gardenId, new TreeInstance(1, 1, 1));
    }
}
