using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleCollectionWin : MonoBehaviour
{
    int bottlesInLevel;
    int bottlesCollected;

    private void Start()
    {
        bottlesInLevel = FindObjectsOfType<Bottle>().Length;
    }

    public void BottlePickedUp()
    {
        bottlesCollected++;

        if (bottlesCollected ==  bottlesInLevel)
        {
            Debug.LogWarning("Game has been won!");
        }
    }
}
