using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ResourceManager
{
    private static SpriteAtlas _objectAtlas;

    public static T GetPref<T>() where T : MonoBehaviour
    {
        return Resources.Load<T>($"Prefabs/{typeof(T).ToString()}");
    }

    public static GameObject GetGameObjPref(string name)
    {
        return Resources.Load<GameObject>($"Prefabs/{name}");
    }

    public static Sprite GetObjectSprite(string name)
    {
        if (_objectAtlas == null)
            _objectAtlas = Resources.Load<SpriteAtlas>("StageAtlas");

        return _objectAtlas.GetSprite(name);
    }
}
