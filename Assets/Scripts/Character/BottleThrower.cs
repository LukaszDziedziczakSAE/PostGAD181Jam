using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BottleThrower : MonoBehaviour
{
    Player player;
    [SerializeField] Bottle bottlePrefab;
    [SerializeField] float throwForce = 10;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Vector3 spawnRotation;

    Bottle bottle;
    float lastThrowTime;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        
    }

    public void SpawnBottle()
    {
        player.Inventory.RemoveBottleFromInventory();
        if (UI.DebugIsOn) UI.Debug.AddText("player position = " + player.transform.position);
        if (UI.DebugIsOn) UI.Debug.AddText("hand position = " + player.HandPosition);
        bottle = Instantiate(bottlePrefab, player.HandPosition, Quaternion.identity);
        if (UI.DebugIsOn) UI.Debug.AddText("bottle position = " + bottle.transform.position);
        bottle.transform.parent = player.RightHand;
        bottle.transform.localScale = Vector3.one * 100;
        bottle.transform.localPosition = spawnPosition;
        bottle.transform.localEulerAngles = spawnRotation;
        bottle.SetModeThrowing();

    }

    public void Throw()
    {
        //Time.timeScale = 0;
        Debug.Log(player.name + ": ThrowForce = " + throwingForce);
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
