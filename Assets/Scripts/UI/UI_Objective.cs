using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Objective : MonoBehaviour
{
    [SerializeField] TMP_Text objectiveText;
    [SerializeField] string objective = "Collect all the bottles";

    public void UpdateObjectiveText()
    {
        objectiveText.text = objective + " (" + Game.BottleCollectionWin.BottlesCollected + "/" + Game.BottleCollectionWin.BottlesInLevel + ")";
    }
}
