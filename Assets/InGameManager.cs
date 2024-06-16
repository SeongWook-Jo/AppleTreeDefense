using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public Transform housePosition;

    public TreeSpawner treeSpawner;

    public EnemySpawner spawner;
    

    private void Awake()
    {
        InfoManager.Init();

        treeSpawner.Init();
        spawner.Init();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
