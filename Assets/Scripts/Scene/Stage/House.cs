using JetBrains.Annotations;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class House : MonoBehaviour, IPointerClickHandler
{
    public float Health { get; private set; }

    private float _currHealth;

    private Action _dieAciton;

    private Action _clickAction;

    private HouseInstance _house;

    public void Init(Action dieAciton, Action clickAction)
    {
        _house = Player.Instance.House;

        _dieAciton = dieAciton;

        _clickAction = clickAction;
    }

    public void Set()
    {
        Health = _house.GetHealth();

        _currHealth = Health;
    }

    public void Hit(float damage)
    {
        _currHealth -= damage;

        if (_currHealth <= 0)
            _dieAciton?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (StageManager.CurrGameState == GameState.Lobby)
        {
            _clickAction?.Invoke();
        }
    }

    public float GetHealthProgress()
    {
        return _currHealth / Health;
    }
}
