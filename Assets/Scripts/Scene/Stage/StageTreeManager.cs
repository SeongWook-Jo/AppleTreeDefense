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

    public void CreateTree()
    {
        foreach(var pos in treePos)
        {
            _tilePref.MakeInstance(pos);
        }

        var idx = 0;

        foreach (var garden in Player.Instance.GardenList)
        {
            var gardenInfo = InfoManager.GardenInfos[garden.Key];

            if (gardenInfo.OpenStage > Player.Instance.LastestClearStage)
                continue;

            if (garden.Value.IsBuy == false)
                continue;

            var treeObj = _treePref.MakeInstance(treePos[idx]);

            treeObj.Init(Player.Instance.TreeList[garden.Key], _createTreeAction);

            _treeList.Add(treeObj);

            idx++;
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
