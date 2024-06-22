using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{ 

    public static bool IsTag(this Collider2D collision, string tag)
    {
        if (collision.CompareTag("tag"))
            return true;

        return false;
    }

    public static T MakeInstance<T>(this T obj, Transform parent) where T : MonoBehaviour
    {
        return Object.Instantiate<T>(obj, parent);
    }
}
