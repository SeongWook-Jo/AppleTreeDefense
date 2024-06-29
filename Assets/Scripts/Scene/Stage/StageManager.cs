using System.Collections;

using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static GameState CurrGameState { get; private set; }

    public Camera gameCamera;

    public StageUiManager uiManager;

    public StageHudManager hudManager;

    public StageTreeManager treeManager;

    public StageEnemyManager enemyManager;

    public Transform housePosition;

    private House _house;

    private StageInfo _stageInfo;

    private int[] _waves;

    private int[] _enemies;

    private int _currWaveIdx;

    private int _stageId;

    private float _waveTerm;

    private float _enemyCreateTime;

    private int _inGameGold;

    private void Awake()
    {
        InfoManager.Init();

        Player.Instance.Load(Init);

        SetGameState(GameState.Lobby);
    }

    private void RefreshGold()
    {
        var currGold = Player.Instance.Gold;

        uiManager.RefreshGold(currGold);
    }

    private void SetGameState(GameState state)
    {
        CurrGameState = state;
    }

    public void Init()
    {
        uiManager.Init(this, GameStart);
        hudManager.Init(this);

        treeManager.Init(this, CreateTreeHud);
        enemyManager.Init(this, GetInGameGold);
        HouseInit();

        treeManager.CreateTrees();

        hudManager.Hide();

        RefreshGold();

        uiManager.ChangeShowType(StageUiManager.ShowType.Stage);
    }

    private void GetInGameGold(Vector3 enemyPos, int dropGold)
    {
        _inGameGold += dropGold;

        var randomPos = Random.insideUnitCircle;

        var originScreenPos = gameCamera.WorldToScreenPoint(enemyPos);

        var secondScreenPos =  gameCamera.WorldToScreenPoint(enemyPos + new Vector3(randomPos.x, randomPos.y));

        uiManager.ShowInGameGoldAnimation(originScreenPos, secondScreenPos, _inGameGold);
    }

    private void GameStart()
    {
        _inGameGold = 0;

        treeManager.RefreshTrees();

        _house.Set();

        hudManager.Show();

        SetGameState(GameState.Playing);

        Debug.LogError("GameStart");

        SetStage(Player.Instance.LastestClearStage);
    }

    private void SetStage(int stageId)
    {
        if (InfoManager.StageInfos.ContainsKey(stageId) == false)
        {
            Debug.LogError("AllStageEnd");

            return;
        }

        Debug.LogError($"Start Stage {stageId}");

        uiManager.SetStage(stageId);

        _stageId = stageId;

        _stageInfo = InfoManager.StageInfos[stageId];

        _waveTerm = _stageInfo.WaveTerm;

        _waves = _stageInfo.Waves;

        SetWave(0);
    }

    private void SetWave(int waveIdx)
    {
        Debug.LogError($"StartWaveIdx {waveIdx}");

        uiManager.SetWave(waveIdx);

        _currWaveIdx = waveIdx;

        var enemies = InfoManager.WaveInfos[_waves[_currWaveIdx]].Enemies;

        enemyManager.CreateEnemies(enemies);
    }

    private void GameEnd()
    {
        Debug.LogError("GameEnd");

        SetGameState(GameState.Lobby);

        if (_stageId > Player.Instance.LastestClearStage)
        {
            Player.Instance.SetLatestStage(_stageId);
        }

        Player.Instance.AddGold(_inGameGold);

        RefreshGold();

        uiManager.GameEnd();

        enemyManager.Clear();
        treeManager.Clear();
        hudManager.Hide();
    }

    private void HouseInit()
    {
        var housePref = ResourceManager.GetPref<House>();

        _house = housePref.MakeInstance(housePosition);

        _house.Init(GameEnd, ClickHouse);

        hudManager.CreateHouseHud(_house);
    }

    private void ClickHouse()
    {
        uiManager.ShowUpgradeHouse();
    }

    public void ClickTreeInLobby(int gardenId)
    {
        uiManager.ShowUpgradeTree(gardenId);
    }

    private void CreateTreeHud(Tree tree)
    {
        hudManager.CreateTreeHud(tree);
    }

    private void Update()
    {
        if (CurrGameState != GameState.Playing)
            return;

        var dt = Time.deltaTime;

        _enemyCreateTime += dt;

        if (_enemyCreateTime > _waveTerm)
        {
            _enemyCreateTime = 0;

            NextWave();
        }

        hudManager.UpdateObjs();
        treeManager.UpdateObjs(dt);
    }

    private void NextWave()
    {
        if (_currWaveIdx >= _waves.Length - 1)
        {
            SetStage(_stageId + 1);

            return;
        }

        var nextWaveIdx = _currWaveIdx + 1;

        SetWave(nextWaveIdx);
    }
}
