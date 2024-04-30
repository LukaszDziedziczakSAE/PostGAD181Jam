using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIS_Investigating : AI_State
{
    Vector3 soundPosition;
    float proximity = 1f;
    float investigationTime = 3f;
    float timer;

    public AIS_Investigating(Enemy enemy, Vector3 soundPosition) : base(enemy)
    {
        this.soundPosition = soundPosition;
    }

    public override void StateStart()
    {
        if (enemy.StatusIndicator != null) enemy.StatusIndicator.Show(false);
        enemy.AI.Sight.OnSeePlayer += OnSeePlayer;
        enemy.AI.Hearing.OnHearFootstep += OnHearFootstep;
        SetNevMeshAgent();
        enemy.NavMeshAgent.speed = enemy.WalkSpeed;

        if (enemy != null && enemy.State != null &&
            enemy.State.GetType() == typeof(CS_Aiming))
        {
            enemy.SetNewState(new CS_Locomotion(enemy));
        }

        timer = 0f;

        Debug.Log(enemy.name + " starting investigation");
    }

    public override void Tick(float deltaTime)
    {
        if (InProximityToDestination) timer += deltaTime;

        if (timer > investigationTime)
        {
            enemy.AI.ResetToStartingState();
        }
    }
    
    public override void StateEnd()
    {
        if (enemy.StatusIndicator != null) enemy.StatusIndicator.Hide();
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
        timer = 0;
        SetNevMeshAgent();
    }

    private void SetNevMeshAgent()
    {
        enemy.NavMeshAgent.SetDestination(soundPosition);
    }

    private bool InProximityToDestination
    {
        get
        {
            return Vector3.Distance(soundPosition, enemy.Position) < proximity;
        }
    }
}
