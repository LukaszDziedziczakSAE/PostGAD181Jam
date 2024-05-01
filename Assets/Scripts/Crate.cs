using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] EAxisOfMovement axisOfMovement;
    [SerializeField] float clearance = 1f;
    [SerializeField] float stopSoundDelay;
    [SerializeField] AudioSource audioSource;
    bool characterEntered;
    bool forward;
    float lastMove = Mathf.NegativeInfinity;

    public EAxisOfMovement AxisOfMovement => axisOfMovement;
    public float Clearance => clearance;

    private void Update()
    {
        if (audioSource.isPlaying)
        {
            if (Time.time - lastMove > stopSoundDelay)
                audioSource.Stop();
        }
        else
        {
            if (Time.time - lastMove < stopSoundDelay)
                audioSource.Play();
        }
    }

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
        Vector3 position = transform.position;
        if (axisOfMovement == EAxisOfMovement.xAxis)
        {
            if (forward) position += (new Vector3(1, 0 ,0) * amount);
            else position += (new Vector3(-1, 0, 0) * amount);
        }
        else if (axisOfMovement == EAxisOfMovement.zAxis)
        {
            if (forward) position += (new Vector3(0, 0, 1) * amount);
            else position += (new Vector3(0, 0, -1) * amount);
        }
        transform.position = position;
        lastMove = Time.time;
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
