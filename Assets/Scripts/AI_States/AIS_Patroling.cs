using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// The character moves from navpoint to navpoint. Back and forth. Untill they see or hear something.
/// </summary>
public class AIS_Patroling : AI_State
{
    public AIS_Patroling(Enemy enemy) : base(enemy) { }


    public override void StateStart()
    {
        if (!enemy.PartolManager.HasWaypoints) enemy.AI.SetNewState(new AIS_Gaurding(enemy));

        if (enemy.StatusIndicator != null) enemy.StatusIndicator.Hide();
        enemy.AI.Hearing.OnHearFootstep += OnHearFootstep;
        enemy.AI.Sight.OnSeePlayer += OnSeePlayer;
        enemy.NavMeshAgent.speed = enemy.WalkSpeed;

        if (enemy != null && enemy.State != null &&
            enemy.State.GetType() == typeof(CS_Aiming))
        {
            enemy.SetNewState(new CS_Locomotion(enemy));
        }

        Debug.Log(enemy.name + " starting patrol");
    }

    public override void Tick(float deltaTime)
    {
        if (enemy.PartolManager.Unset || enemy.PartolManager.InProximityToCurrentWaypoint)
        {
            enemy.PartolManager.SetNextWaypoint();
            
        }
        else if (!enemy.PartolManager.IsWaiting && enemy.NavMeshAgent.destination != enemy.PartolManager.CurrentWaypoint.Position)
        {
            enemy.NavMeshAgent.SetDestination(enemy.PartolManager.CurrentWaypoint.Position);
        }
        else if (enemy.PartolManager.IsWaiting)
        {
            enemy.NavMeshAgent.destination = enemy.Position;

        }
    }

    public override void StateEnd()
    {
        enemy.AI.Sight.OnSeePlayer -= OnSeePlayer;
        enemy.AI.Hearing.OnHearFootstep -= OnHearFootstep;
    }



    private void OnHearFootstep(Vector3 soundPosition)
    {
        Debug.Log(enemy.name + " heard footstep at " + soundPosition.ToString());
        enemy.AI.SetNewState(new AIS_Investigating(enemy, soundPosition));
    }

    private void OnSeePlayer(Player player)
    {
        enemy.AI.SetNewState(new AIS_Engaging(enemy, player));
    }
}
