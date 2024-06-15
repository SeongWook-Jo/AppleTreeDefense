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

    public Dictionary<byte, TreeInstance> TreeList;

    private void Init()
    {
        TreeList = new Dictionary<byte, TreeInstance>();

        TestSet();
    }

    private void TestSet()
    {


        TreeList.Add(1, new TreeInstance(1, true));
    }
}
