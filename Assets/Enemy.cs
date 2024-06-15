using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public float speed;

    private bool _isAttack;

    public void Hit()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {

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
            transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void Attack(House house)
    {
        _isAttack = true;
        house.Hit();
    }
}
