using System;
using System.Collections.Generic;
using System.Diagnostics;

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

    #region Values
    
    public int Gold { get; private set; }
    public int LastestClearStage;

    #endregion

    #region Instance
    public HouseInstance House;
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
        House = new HouseInstance();
    }

    private void CreateNewAccountForTest()
    {
        for (int i = 1; i < 5; i++)
        {
            _gardenList.Add(i, new GardenInstance(true));
            _treeList.Add(i, new TreeInstance(1, 1, 1));
        }

        LastestClearStage = 10;
        
        House.SetHealthLevel(1);

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
}
