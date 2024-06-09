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
        treeSpawner.Init();
        spawner.Init();
    }

    void Start()
    {
        
    }

    void Update()
    {
        //inputmanager

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit;

            var mainCam = Camera.main;

            var mousePo = Input.mousePosition;

            var rayPo = mainCam.ScreenPointToRay(mousePo);

            rayHit = Physics2D.Raycast(rayPo.origin, rayPo.direction);

            if (rayHit.collider == null)
                return;

            var tree = rayHit.collider.gameObject.GetComponent<Tree>();

            if (tree == null)
                return;

            tree.OnClick();
        }
    }
}
