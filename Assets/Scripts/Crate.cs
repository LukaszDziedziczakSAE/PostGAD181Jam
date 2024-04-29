using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    bool characterEntered;

    public enum EAxisOfMovement
    {
        xAxis,
        zAxis
    }

    private void OnTriggerEnter(Collider other)
    {
        if (characterEntered) return;

        if (other.TryGetComponent<Player>(out Player player))
        {
            characterEntered = true;
            player.SetNewState(new CS_Pushing(player, this));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            characterEntered = false;
        }
    }
}
