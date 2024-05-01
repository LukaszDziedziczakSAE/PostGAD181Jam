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
        if (player.Targeting.HasTarget)
        {
            bottle.SetModeTargeted(player.Targeting.Target, throwingForce);
        }
        else
        {
            bottle.SetModeInFlight(throwingForce);
        }
        
        bottle = null;
    }

    private Vector3 throwingForce => (player.transform.forward + (player.transform.up / 2)) * throwForce;
}
