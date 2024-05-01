using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    Character character;

    public event Action<Vector3> OnWalkStep;
    public event Action<Vector3> OnRunStep;

    private void Awake()
    {
        character = GetComponentInParent<Character>();
    }

    private void WalkStep()
    {
        //Debug.Log(player.name + " footstep");
        OnWalkStep?.Invoke(character.Position);
        character.SFX_Footstep.PlayWalkingFootstep();
    }

    private void RunStep()
    {
        OnRunStep?.Invoke(character.Position);
        character.SFX_Footstep.PlayRunningFootstep();
    }
}
