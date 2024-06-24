using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public static T GetPref<T>() where T : MonoBehaviour
    {
        return Resources.Load<T>($"Prefabs/{typeof(T).ToString()}");
    }

    public static GameObject GetGameObjPref(string name)
    {
        return Resources.Load<GameObject>($"Prefabs/{name}");
    }
}
