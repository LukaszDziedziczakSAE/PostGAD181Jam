using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    private EMode mode;

    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Vector3 spawnRotation;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider _collider;

    public enum EMode
    {
        Pickup,
        Throwing,
        InFight
    }

    private void OnTriggerEnter(Collider other)
    {
        if (mode == EMode.Pickup)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Inventory.AddBottleToInventory();
                Destroy(gameObject);
            }
        }
        
    }

    public void SetModeThrowing(Player player)
    {
        mode = EMode.Throwing;
        _collider.enabled = false;
        transform.parent = player.RightHand;
        transform.localPosition = spawnPosition;
        transform.localEulerAngles = spawnRotation;
    }

    public void SetModeInFlight(Vector3 newVelocity)
    {
        mode = EMode.InFight;
        _collider.enabled = true;
        transform.parent = null;

        rb.velocity = newVelocity;
    }
}
