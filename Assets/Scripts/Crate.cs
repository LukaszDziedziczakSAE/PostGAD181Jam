using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] EAxisOfMovement axisOfMovement;
    bool characterEntered;
    bool forward;

    public EAxisOfMovement AxisOfMovement => axisOfMovement;

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
            SetDirection(player);
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

    public void Move(float amount)
    {
        if (axisOfMovement == EAxisOfMovement.xAxis)
        {
            
        }
        else if (axisOfMovement == EAxisOfMovement.zAxis)
        {
            
        }
    }

    private void SetDirection(Player player)
    {
        if (axisOfMovement == EAxisOfMovement.xAxis)
        {
            if (player.transform.position.x < transform.position.x) forward = true;
            else forward = false;
        }
        else if (axisOfMovement == EAxisOfMovement.zAxis)
        {
            if (player.transform.position.z < transform.position.z) forward = true;
            else forward = false;
        }
    }

    private float PlayerFaceDirection
    {
        get
        {

        }
    }
}
