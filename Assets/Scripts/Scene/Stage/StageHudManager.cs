using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class StageHudManager : MonoBehaviour
{
    private StageHudTreeObj _treeHudObjPref;

    private List<StageHudTreeObj> _treeHudObjList;

    private StageHudHouseObj _houseHudObj;

    private StageManager _manager;

    public void Init(StageManager manager)
    {
        _manager = manager;
        _treeHudObjPref = ResourceManager.GetPref<StageHudTreeObj>();
        _treeHudObjList = new List<StageHudTreeObj>();
    }

    public void CreateTreeHud(Tree tree)
    {
        var hudObj = _treeHudObjPref.MakeInstance(gameObject.transform);

        hudObj.SetTree(_manager.gameCamera, tree);

        _treeHudObjList.Add(hudObj);
    }

    public void CreateHouseHud(House house)
    {
        _houseHudObj = ResourceManager.GetPref<StageHudHouseObj>().MakeInstance(gameObject.transform);

        _houseHudObj.Init(house);

        var ScreenPos = _manager.gameCamera.WorldToScreenPoint(house.transform.position + Vector3.up * 0.6f);

        _houseHudObj.transform.position = ScreenPos;
    }

    public void UpdateObjs()
    {
        foreach (var obj in _treeHudObjList)
        {
            obj.UpdateProgree();
        }

        _houseHudObj.UpdateProgress();
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
