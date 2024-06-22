using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Camera gameCamera;

    public StageUiManager uiManager;

    public StageHudManager hudManager;

    public StageTreeManager stageTreeManager;

    public StageEnemyManager stageEnemyManager;

    public Transform housePosition;

    private void Awake()
    {
        InfoManager.Init();

        Player.Instance.Load(Init);
    }

    public void Init()
    {
        stageTreeManager.Init(CreateTreeHud);
        stageEnemyManager.Init();
        hudManager.Init();
        HouseInit();

        GameStart();
    }

    private void GameStart()
    {
        stageTreeManager.CreateTree();
    }

    private void HouseInit()
    {
        var housePref = ResourceManager.GetPref<House>();
        var house = housePref.MakeInstance(housePosition);

        house.Init();
    }

    private void CreateTreeHud(Tree tree)
    {
        hudManager.CreateTreeHud(gameCamera, tree);
    }

    private void Update()
    {
        hudManager.UpdateObjs();
    }
}
