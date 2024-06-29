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

    public int Gold => _gold;
    private int _gold;

    public int LastestClearStage => _lastestClearStage;
    private int _lastestClearStage;

    #endregion

    #region Instance
    public HouseInstance House => _house;

    private HouseInstance _house;

    public Dictionary<int, GardenInstance> GardenList => _gardenList;

    private Dictionary<int, GardenInstance> _gardenList;

    public Dictionary<int, TreeInstance> TreeList => _treeList;

    private Dictionary<int, TreeInstance> _treeList;

    #endregion

    private Action _loadEndAction;

    public void Init()
    {
        _gardenList = new Dictionary<int, GardenInstance>();
        _treeList = new Dictionary<int, TreeInstance>();
        _house = new HouseInstance();
    }

    private void CreateNewAccountForTest()
    {
        for (int i = 1; i < 5; i++)
        {
            _gardenList.Add(i, new GardenInstance(true));
            _treeList.Add(i, new TreeInstance(1, 1, 1));
        }

        _lastestClearStage = 1;

        House.SetHealthLevel(1);

        _gold = 0;

        UnityEngine.Debug.LogError("CreateNewAccountForTest");
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
        _lastestClearStage = stage;
    }

    public void AddGold(int gold)
    {
        _gold += gold;

        Mathf.Clamp(_gold, 0, int.MaxValue);
    }
}
