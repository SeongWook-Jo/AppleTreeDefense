using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Player");

                _instance = go.AddComponent<Player>();

                _instance.Init();

                DontDestroyOnLoad(go);
            }

            return _instance;
        }
    }

    private static Player _instance;

    private List<TreeInstance> _treeList;

    private void Init()
    {
        _treeList = new List<TreeInstance>();
    }
}
