using UnityEngine;
using UnityEngine.Pool;

public class StageEnemyManager : MonoBehaviour
{
    public Transform enemyPos;

    public float createTime;

    public float enemySpeed;

    private ObjectPool<Enemy> _enemyPool;

    private Enemy _enemyPref;

    private float _tempTime;

    public void Init()
    {
        _enemyPref = ResourceManager.GetPref<Enemy>();

        _enemyPool = new ObjectPool<Enemy>(CreateEnemy, OnGet, OnRelease);
    }

    private void Update()
    {
        _tempTime += Time.deltaTime;

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

        enemy.gameObject.SetActive(true);
    }

    private void OnRelease(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void ReturnToPool(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }
}
