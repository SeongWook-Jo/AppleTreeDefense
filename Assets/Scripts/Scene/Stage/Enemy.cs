using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    enum State
    {
        None,
        Alive,
        Attack,
    }
    public EnemyInfo Info { get; private set; }

    private State _state;

    private float _currHealth;

    private float _attackTime;

    private House _target;

    private Action<Enemy> _dieAction;

    public void Init(Action<Enemy> dieAction)
    {
        _dieAction = dieAction;
    }

    public void Set(EnemyInfo info)
    {
        SetState(State.Alive);

        Info = info;

        _currHealth = info.Health;
    }

    private void SetState(State state)
    {
        _state = state;
    }

    public void OnRelease()
    {
        SetState(State.None);
        Info = null;
        _currHealth = 0;
    }

    public void SetRandomPos(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public bool Hit(float damage)
    {
        if (gameObject.activeSelf == false)
            return false;

        _currHealth -= damage;

        if (_currHealth <= 0)
            _dieAction?.Invoke(this);

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
        var dt = Time.fixedDeltaTime;

        switch (_state)
        {
            case State.Alive:
                {
                    transform.Translate(Vector3.left * dt * Info.Speed);
                }
                break;
            case State.Attack:
                {
                    _attackTime += dt;

                    if (_attackTime > Info.AttackTime)
                    {
                        Attack(_target);
                    }
                }
                break;
            default:
                {

                }
                break;
        }


    }

    private void Attack(House house)
    {
        _target = house;

        _attackTime = 0;

        SetState(State.Attack);

        house.Hit(Info.Damage);
    }
}
