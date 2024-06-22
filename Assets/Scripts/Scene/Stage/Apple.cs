using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public Collider2D collider;

    public float dropSpeed;
    public bool IsDrop { get; private set; }

    public void Drop()
    {
        collider.enabled = true;
        IsDrop = true;
    }

    private void FixedUpdate()
    {
        if (IsDrop)
        {
            transform.Translate(new Vector3(0, -dropSpeed, 0) * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        if (collision.gameObject == null) return;

        var enemy = collision.GetComponent<Enemy>();

        if (enemy == null) return;

        if (enemy.Hit())
            Destroy(gameObject);
    }
}
