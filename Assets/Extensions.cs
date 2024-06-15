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
}
