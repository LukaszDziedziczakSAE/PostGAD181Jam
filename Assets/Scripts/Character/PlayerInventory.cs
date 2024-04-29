using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [field: SerializeField] public int BottlesCollected { get; private set; }
    
    public void AddBottleToInventory()
    {
        BottlesCollected++;
        UI.BottleCountIndicator.UpdateBottlesCollected();
    }
}
