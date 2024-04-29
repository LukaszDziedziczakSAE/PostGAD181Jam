using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    Player player;

    public event Action<Vector3> OnWalkStep;
    public event Action<Vector3> OnRunStep;

    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    private void WalkStep()
    {
        OnWalkStep?.Invoke(player.Position);
        player.SFX_Footstep.PlayFootstepSound();
    }

    private void RunStep()
    {
        OnRunStep?.Invoke(player.Position);
        player.SFX_Footstep.PlayFootstepSound();
    }
}