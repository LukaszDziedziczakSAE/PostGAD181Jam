using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleThrower : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Bottle bottlePrefab;
    [SerializeField] float throwForce = 10;

    Bottle bottle;

    public void SpawnBottle()
    {
        player.Inventory.RemoveBottleFromInventory();
        bottle = Instantiate(bottlePrefab);
        bottle.SetModeThrowing(player);
    }

    public void Throw()
    {
        Debug.Log("Throw called");
        bottle.SetModeInFlight((player.transform.forward + (player.transform.up / 2) )* throwForce);
        bottle = null;
    }
}
