using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] EAxisOfMovement axisOfMovement;
    [SerializeField] float clearance = 1f;
    bool characterEntered;
    bool forward;

    public EAxisOfMovement AxisOfMovement => axisOfMovement;
    public float Clearance => clearance;

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

    public float PlayerFaceDirection
    {
        get
        {
            if (axisOfMovement == EAxisOfMovement.xAxis)
            {
                if (forward) return 90;
                else return (90 * 3);
            }
            else if (axisOfMovement == EAxisOfMovement.zAxis)
            {
                if (forward) return 0;
                else return 180f;
            }
            else return 0;
        }
    }

    public float PlayerPosition
    {
        get
        {
            if (axisOfMovement == EAxisOfMovement.xAxis)
            {
                if (forward) return transform.position.x - clearance;
                else return transform.position.x + clearance;
            }
            else if (axisOfMovement == EAxisOfMovement.zAxis)
            {
                if (forward) return transform.position.z - clearance;
                else return transform.position.z + clearance;
            }
            else return 0;
        }
    }
}
