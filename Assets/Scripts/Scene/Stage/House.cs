using JetBrains.Annotations;
using System;
using UnityEngine;

public class House : MonoBehaviour
{
    public float Health { get; private set; }

    public int currHealth;

    private Action _dieAciton;

    public void Init(Action dieAciton)
    {
        var house = Player.Instance.House;

        Health = house.GetHealth();

        _dieAciton = dieAciton;

        currHealth = 2;
    }

    public void Hit()
    {
        currHealth--;

        if (currHealth <= 0)
            _dieAciton?.Invoke();
    }
}
