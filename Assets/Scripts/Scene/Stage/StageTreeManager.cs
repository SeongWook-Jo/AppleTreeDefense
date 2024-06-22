using System;
using UnityEngine;

public class StageTreeManager : MonoBehaviour
{
    public Transform[] treePos;

    private Tree _treePref;

    private Action<Tree> _createTreeAction;

    public void Init(Action<Tree> createTreeAction)
    {
        _treePref = ResourceManager.GetPref<Tree>();

        _createTreeAction = createTreeAction;
    }

    public void CreateTree()
    {
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

            idx++;
        }
    }
}
