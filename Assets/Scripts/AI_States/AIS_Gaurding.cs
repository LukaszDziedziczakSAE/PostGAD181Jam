using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIS_Gaurding : AI_State
{
    public AIS_Gaurding(Enemy enemy) : base(enemy)
    {
    }

    public override void StateStart()
    {
        enemy.StatusIndicator.Hide();
        enemy.AI.Hearing.OnHearFootstep += OnHearFootstep;
        enemy.AI.Sight.OnSeePlayer += OnSeePlayer;
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
        enemy.AI.Sight.OnSeePlayer -= OnSeePlayer;
        enemy.AI.Hearing.OnHearFootstep -= OnHearFootstep;
    }

    private void OnHearFootstep(Vector3 soundPosition)
    {
        enemy.AI.SetNewState(new AIS_Investigating(enemy, soundPosition));
    }

    private void OnSeePlayer(Player player)
    {
        enemy.AI.SetNewState(new AIS_Engaging(enemy, player));
    }
}
