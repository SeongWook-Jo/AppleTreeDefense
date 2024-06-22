using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    private float _speed;

    private bool _isAttack;

    private Action<Enemy> _releaseAction;

    public void Init(Action<Enemy> releaseAction)
    {
        _releaseAction = releaseAction;
    }

    public void Set(Vector3 pos, float speed)
    {
        _isAttack = false;
        _speed = speed;
        transform.localPosition = pos;
    }

    public bool Hit()
    {
        if (gameObject.activeSelf == false)
            return false;

        _releaseAction?.Invoke(this);

        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var house = collision.GetComponent<House>();

        if (house == null)
            return;

        Attack(house);
    }

    private void FixedUpdate()
    {
        if (_isAttack == false)
            transform.Translate(Vector3.left * Time.fixedDeltaTime * _speed);
    }

    private void Attack(House house)
    {
        _isAttack = true;

        house.Hit();
    }
}
