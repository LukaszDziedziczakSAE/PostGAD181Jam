using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Hearing : MonoBehaviour
{
    [SerializeField] private AI ai;
    [SerializeField] float walkingHearingRange;
    [SerializeField] float runningHearingRange;
    [SerializeField] float firearmHearingRange;

    public bool HearFootsteps { get; private set; } = false;

    public event Action<Vector3> OnHearFootstep;

    private void Update()
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkingHearingRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, runningHearingRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, firearmHearingRange);
    }

    public void OnHearWalkStep(Vector3 postion)
    {
        if (Distance(postion) <= walkingHearingRange)
        {
            //Debug.Log(ai.Enemy.name + " heard footstep at " + postion.ToString());
            OnHearFootstep?.Invoke(postion);
        }

    }

    public void OnHearRunStep(Vector3 postion)
    {
        if (Distance(postion) <= runningHearingRange)
        {
            OnHearFootstep?.Invoke(postion);
        }
    }

    public void OnHearFirearm()
    {

    }

    private float Distance(Vector3 position)
    {
        return Vector3.Distance(ai.Enemy.Position, position);
    }

}
