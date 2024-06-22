using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private bool _isAttack;

    private Action<Enemy> _releaseAction;

    public void Init(Action<Enemy> releaseAction)
    {
        _releaseAction = releaseAction;
    }

    public void Set()
    {
        _isAttack = false;
    }

    public bool Hit()
    {
        if (gameObject.activeSelf == false)
            return false;

        gameObject.SetActive(false);

        Debug.LogError($"Hit {gameObject.GetInstanceID()}");

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
            transform.Translate(Vector3.left * Time.fixedDeltaTime * speed);
    }

    private void Attack(House house)
    {
        _isAttack = true;

        house.Hit();
    }
}
