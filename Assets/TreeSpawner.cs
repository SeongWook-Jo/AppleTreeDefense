using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public Transform[] treePos;

    public void Init()
    {
        var treePref = Resources.Load<Tree>("Prefabs/Tree");

        var idx = 0;

        foreach(var tree in Player.Instance.TreeList.Values)
        {
            if (tree.IsOpen == false)
                continue;

            var treeObj = Instantiate(treePref, treePos[idx].position, Quaternion.identity);

            treeObj.Init();

            idx++;
        }
    }
}
