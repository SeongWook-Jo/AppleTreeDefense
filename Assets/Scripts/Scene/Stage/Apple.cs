using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private static readonly float MaxDropTime = 5f;


    private bool _isDrop;

    private float _dropSpeed;

    private float _dropTime;

    private Action<Apple> _releaseAction;

    public void Init(Action<Apple> releaseAction)
    {
        _releaseAction = releaseAction;
    }

    public void Set(Vector3 pos, float dropSpeed)
    {
        transform.localPosition = pos;

        _isDrop = false;

        _dropSpeed = dropSpeed;
    }

    public void Drop()
    {
        _dropTime = 0;

        _isDrop = true;
    }

    private void FixedUpdate()
    {
        if (_isDrop)
        {
            _dropTime += Time.fixedDeltaTime;

            if (_dropTime > MaxDropTime )
            {
                _releaseAction?.Invoke(this);

                return;
            }

            transform.Translate(new Vector3(0, -(_dropSpeed), 0) * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject == null) return;

        var enemy = collision.GetComponent<Enemy>();

        if (enemy == null) return;

        if (enemy.Hit())
            _releaseAction?.Invoke(this);
    }
}
