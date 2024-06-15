using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeInstance
{
    public TreeInstance(int id, bool isOpen)
    {
        Id = id;
        IsOpen = isOpen;
    }

    public int Id { get; private set; }
    public bool IsOpen { get; private set; }
    

}
