using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Camera gameCamera;

    public StageUiManager uiManager;

    public StageHudManager hudManager;

    public StageTreeManager stageTreeManager;

    public StageEnemyManager stageEnemyManager;

    public Transform housePosition;

    private bool _isGameStart;

    private void Awake()
    {
        InfoManager.Init();

        Player.Instance.Load(Init);
    }

    public void Init()
    {
        uiManager.Init(GameStart);
        hudManager.Init();

        stageTreeManager.Init(CreateTreeHud);
        stageEnemyManager.Init();
        HouseInit();

        stageTreeManager.CreateTree();

        hudManager.Hide();
    }

    private void GameStart()
    {
        _isGameStart = true;

        hudManager.Show();
    }

    private void GameEnd()
    {
        if (_isGameStart == false)
            return;

        _isGameStart = false;

        stageEnemyManager.Clear();
        stageTreeManager.Clear();
        hudManager.Hide();

        uiManager.ChangeShowType(StageUiManager.ShowType.Ready);
    }

    private void HouseInit()
    {
        var housePref = ResourceManager.GetPref<House>();
        var house = housePref.MakeInstance(housePosition);

        house.Init(GameEnd);
    }

    private void CreateTreeHud(Tree tree)
    {
        hudManager.CreateTreeHud(gameCamera, tree);
    }

    private void Update()
    {
        if (_isGameStart == false)
            return;

        var dt = Time.deltaTime;

        hudManager.UpdateObjs();
        stageEnemyManager.UpdateObjs(dt);
        stageTreeManager.UpdateObjs(dt);
    }
}
