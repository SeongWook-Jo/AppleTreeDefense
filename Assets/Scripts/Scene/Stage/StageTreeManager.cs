using System;
using System.Collections.Generic;
using UnityEngine;

public class StageTreeManager : MonoBehaviour
{
    public Transform[] treePos;

    private Tree _treePref;

    private GameObject _tilePref;

    private List<Tree> _treeList;

    private Action<Tree> _createTreeAction;

    private StageManager _manager;

    public void Init(StageManager manager, Action<Tree> createTreeAction)
    {
        _manager = manager;

        _treePref = ResourceManager.GetPref<Tree>();

        _tilePref = ResourceManager.GetGameObjPref("Tile");

        _treeList = new List<Tree>();

        _createTreeAction = createTreeAction;
    }

    public void CreateTrees()
    {
        var gardenKey = 1;

        foreach (var treetran in treePos)
        {
            var gardenInfo = InfoManager.GardenInfos[gardenKey];

            if (gardenInfo.OpenStage > Player.Instance.LastestClearStage)
                continue;

            var treeObj = _treePref.MakeInstance(treetran);

            treeObj.Init(gardenKey, _createTreeAction, _manager.ClickTreeInLobby);

            _treeList.Add(treeObj);

            gardenKey++;
        }

        RefreshTrees();
    }

    public void RefreshTrees()
    {
        foreach (var tree in _treeList)
        {
            tree.Refresh();
        }
    }

    public void Clear()
    {
        foreach (var tree in _treeList)
            tree.Clear();
    }
    
    public void UpdateObjs(float dt)
    {
        foreach (var tree in _treeList)
            tree.UpdateObj(dt);
    }
}
