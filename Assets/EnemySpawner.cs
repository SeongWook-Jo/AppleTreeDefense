using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float createTime;

    public float enemySpeed;

    private float _tempTime;

    private Enemy _enemyPref;

    public void Init()
    {
        _enemyPref = Resources.Load<Enemy>("Prefabs/Enemy");
    }

    private void Update()
    {
        _tempTime += Time.deltaTime;

        if (_tempTime > createTime)
        {
            _tempTime = 0;

            CreateEnemy();
        }
    }

    private void CreateEnemy()
    {
        var randomYPos = Random.Range(0, 0.5f);

        var enemy = Instantiate(_enemyPref, transform.position + new Vector3(0, randomYPos, 0), Quaternion.identity);

        enemy.speed = enemySpeed;
    }
}
