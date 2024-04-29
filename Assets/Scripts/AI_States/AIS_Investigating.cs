using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIS_Investigating : AI_State
{
    Vector3 soundPosition;

    public AIS_Investigating(Enemy enemy, Vector3 soundPosition) : base(enemy)
    {
        this.soundPosition = soundPosition;
    }

    public override void StateStart()
    {
        enemy.StatusIndicator.Show(false);
        enemy.AI.Sight.OnSeePlayer += OnSeePlayer;
        enemy.AI.Hearing.OnHearFootstep += OnHearFootstep;
        SetNevMeshAgent();
        enemy.NavMeshAgent.speed = enemy.WalkSpeed;

        if (enemy != null && enemy.State != null &&
            enemy.State.GetType() == typeof(CS_Aiming))
        {
            enemy.SetNewState(new CS_Locomotion(enemy));
        }
    }

    public override void Tick(float deltaTime)
    {
    }
    
    public override void StateEnd()
    {
        enemy.StatusIndicator.Hide();
        enemy.AI.Sight.OnSeePlayer -= OnSeePlayer;
        enemy.AI.Hearing.OnHearFootstep -= OnHearFootstep;
    }

    private void OnSeePlayer(Player player)
    {
        enemy.AI.SetNewState(new AIS_Engaging(enemy, player));
    }

    private void OnHearFootstep(Vector3 soundPosition)
    {
        //enemy.AI.SetNewState(new AIS_Investigating(enemy, soundPosition));
        this.soundPosition = soundPosition;
        SetNevMeshAgent();
    }

    private void SetNevMeshAgent()
    {
        enemy.NavMeshAgent.SetDestination(soundPosition);
    }

}
