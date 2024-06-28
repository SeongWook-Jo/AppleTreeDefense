using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public SpriteRenderer sprite;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            sprite.sprite = Resources.Load<Sprite>("apple");
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            sprite.sprite = Resources.Load<Sprite>("Circle");
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            sprite.sprite = Resources.Load<Sprite>("Square");
    }
}
