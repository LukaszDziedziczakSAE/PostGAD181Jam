using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_BottleCountIndicator : MonoBehaviour
{
    [SerializeField] TMP_Text bottlesCollected;


    private void Start()
    {
        UpdateBottlesCollected();
    }
    public void UpdateBottlesCollected()
    {
        bottlesCollected.text= /*"Bottles: " +*/ Player.Instance.Inventory.BottlesCollected.ToString();
    }
}
