using UnityEngine;

public class House : MonoBehaviour
{
    public float Health { get; private set; }
    
    public void Init()
    {
        var house = Player.Instance.House;

        Health = house.GetHealth();
    }

    public void Hit()
    {

    }
}
