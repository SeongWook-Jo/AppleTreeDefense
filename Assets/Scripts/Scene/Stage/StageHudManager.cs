using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHudManager : MonoBehaviour
{
    private StageTreeHudObj _treeHudObjPref;

    private List<StageTreeHudObj> _treeHudObjList;

    private StageManager _manager;

    public void Init(StageManager manager)
    {
        _manager = manager;
        _treeHudObjPref = ResourceManager.GetPref<StageTreeHudObj>();
        _treeHudObjList = new List<StageTreeHudObj>();
    }

    public void CreateTreeHud(Camera camera, Tree tree)
    {
        var hudObj = _treeHudObjPref.MakeInstance(gameObject.transform);

        hudObj.SetTree(camera, tree);

        _treeHudObjList.Add(hudObj);
    }

    public void UpdateObjs()
    {
        foreach (var obj in _treeHudObjList)
        {
            obj.UpdateProgree();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
