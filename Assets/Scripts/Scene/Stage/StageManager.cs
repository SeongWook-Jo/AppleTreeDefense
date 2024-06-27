using System.Collections;

using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static GameState CurrGameState { get; private set; }

    public Camera gameCamera;

    public StageUiManager uiManager;

    public StageHudManager hudManager;

    public StageTreeManager stageTreeManager;

    public StageEnemyManager stageEnemyManager;

    public Transform housePosition;

    private House _house;

    private StageInfo _stageInfo;

    private int[] _waves;

    private int[] _enemies;

    private int _currWaveIdx;

    private int _stageId;

    private float _waveTerm;

    private float _enemyCreateTime;

    private void Awake()
    {
        InfoManager.Init();

        Player.Instance.Load(Init);

        SetGameState(GameState.Lobby);
    }

    private void SetGameState(GameState state)
    {
        CurrGameState = state;
    }

    public void Init()
    {
        uiManager.Init(this, GameStart);
        hudManager.Init(this);

        stageTreeManager.Init(this, CreateTreeHud);
        stageEnemyManager.Init(this);
        HouseInit();

        stageTreeManager.CreateTree();

        hudManager.Hide();
    }

    private void GameStart()
    {
        _house.Set();

        SetGameState(GameState.Playing);

        hudManager.Show();

        Debug.LogError("GameStart");

        SetStage(1);
    }


    //stageinfo waves -> wave -> enemies

    private void SetStage(int stageId)
    {
        if (InfoManager.StageInfos.ContainsKey(stageId) == false)
        {
            Debug.LogError("StageEnd");

            return;
        }

        Debug.LogError($"Start Stage {stageId}");

        _stageId = stageId;

        _stageInfo = InfoManager.StageInfos[stageId];

        _waveTerm = _stageInfo.WaveTerm;
        _waves = _stageInfo.Waves;

        SetWave(0);
    }

    private void SetWave(int waveIdx)
    {
        Debug.LogError($"StartWaveIdx {waveIdx}");

        _currWaveIdx = waveIdx;

        var enemies = InfoManager.WaveInfos[_waves[_currWaveIdx]].Enemies;

        stageEnemyManager.CreateEnemies(enemies);
    }

    private void GameEnd()
    {
        Debug.LogError("GameEnd");

        SetGameState(GameState.Lobby);

        stageEnemyManager.Clear();
        stageTreeManager.Clear();
        hudManager.Hide();

        uiManager.ChangeShowType(StageUiManager.ShowType.Ready);
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
        Debug.LogError("clickhouse");
    }

    public void ClickTreeInLobby(int gardenId)
    {
        Debug.LogError($"clicktreeinlobby id {gardenId}");
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
        stageTreeManager.UpdateObjs(dt);
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
