using System;

[Serializable]
public class GardenInstance
{
   public GardenInstance(bool isBuy)
   {
      IsBuy = isBuy;
   }
   public bool IsBuy { get; private set; }
}
