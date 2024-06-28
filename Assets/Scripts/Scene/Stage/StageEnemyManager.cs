using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class StageEnemyManager : MonoBehaviour
{
    public Transform enemyPos;

    private ObjectPool<Enemy> _enemyPool;

    private List<Enemy> _activeEnemyList;

    private Enemy _enemyPref;

    private StageManager _manager;

    private Action<Vector3, int> _enemyDieAction;

    public void Init(StageManager manager, Action<Vector3, int> enemyDieAction)
    {
        _manager = manager;

        _enemyDieAction = enemyDieAction;

        _enemyPref = ResourceManager.GetPref<Enemy>();

        _activeEnemyList = new List<Enemy>();

        _enemyPool = new ObjectPool<Enemy>(CreateEnemy, OnGet, OnRelease, OnDestroyObj);
    }

    public void CreateEnemies(int[] enemies)
    {
        foreach (var enemyId in enemies)
        {
            var enemy = _enemyPool.Get();
            enemy.Set(InfoManager.EnemyInfos[enemyId]);
        }
    }

    private Enemy CreateEnemy()
    {
        var enemy = _enemyPref.MakeInstance(enemyPos);
        enemy.Init(EnemyDieAction);
        return enemy;
    }

    private void OnGet(Enemy enemy)
    {
        var randomYPos = UnityEngine.Random.Range(-0.5f, 0.5f);

        enemy.SetRandomPos(Vector3.up * randomYPos);

        _activeEnemyList.Add(enemy);

        enemy.gameObject.SetActive(true);
    }

    private void OnRelease(Enemy enemy)
    {
        _activeEnemyList.Remove(enemy);

        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyObj(Enemy enemy)
    {
        Destroy(enemy);
    }

    private void ReturnToPool(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }

    private void EnemyDieAction(Enemy enemy)
    {
        _enemyDieAction?.Invoke(enemy.transform.position, enemy.Info.DropGold);
        //dropGold, missionCheck, 
        ReturnToPool(enemy);
    }

    public void Clear()
    {
        while (_activeEnemyList.Count > 0)
            _enemyPool.Release(_activeEnemyList[0]);
    }
}
