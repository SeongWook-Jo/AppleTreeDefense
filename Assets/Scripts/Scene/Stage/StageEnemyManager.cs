using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class StageEnemyManager : MonoBehaviour
{
    public Transform enemyPos;

    public float createTime;

    public float enemySpeed;

    private ObjectPool<Enemy> _enemyPool;

    private List<Enemy> _activeEnemyList;

    private Enemy _enemyPref;

    private float _tempTime;

    public void Init()
    {
        _enemyPref = ResourceManager.GetPref<Enemy>();

        _activeEnemyList = new List<Enemy>();

        _enemyPool = new ObjectPool<Enemy>(CreateEnemy, OnGet, OnRelease, OnDestroyObj);
    }

    public void UpdateObjs(float dt)
    {
        _tempTime += dt;

        if (_tempTime > createTime)
        {
            _tempTime = 0;

            _enemyPool.Get();
        }
    }

    private Enemy CreateEnemy()
    {
        var enemy = _enemyPref.MakeInstance(enemyPos);
        enemy.Init(ReturnToPool);
        return enemy;
    }

    private void OnGet(Enemy enemy)
    {
        var randomYPos = Random.Range(-0.5f, 0.5f);

        enemy.Set(Vector3.up * randomYPos, enemySpeed);

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

    public void Clear()
    {
        while (_activeEnemyList.Count > 0)
            _enemyPool.Release(_activeEnemyList[0]);
    }
}
