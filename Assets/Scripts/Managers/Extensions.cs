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

    public static GameObject MakeInstance(this GameObject obj, Transform parent)
    {
        return Object.Instantiate(obj, parent);
    }

    public static void On<T>(this T obj) where T : MonoBehaviour
    {
        obj.gameObject.SetActive(true);
    }

    public static void Off<T>(this T obj) where T : MonoBehaviour
    {
        obj.gameObject.SetActive(false);
    }
}
