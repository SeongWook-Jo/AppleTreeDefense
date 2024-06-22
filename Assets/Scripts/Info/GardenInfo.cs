using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenInfo
{
    public GardenInfo(int openStage, int buyGold)
    {
        OpenStage = openStage;
        BuyGold = buyGold;
    }
    
    public int OpenStage { get; private set; }
    public int BuyGold { get; private set; }
}
