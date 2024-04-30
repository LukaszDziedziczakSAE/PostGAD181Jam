using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIS_Engaging : AI_State
{
    Character target;
    float lastBustTime;
    float burstIndex;
    Vector3 targetPosition;

    public AIS_Engaging(Enemy enemy, Character target) : base(enemy)
    {
        this.target = target;
    }

    public override void StateStart()
    {
        if (enemy.StatusIndicator != null) enemy.StatusIndicator.Show(true);
        enemy.NavMeshAgent.speed = enemy.RunSpeed;
        enemy.AI.SetTarget(target);
        lastBustTime = Time.time - enemy.BurstRate;
        enemy.WeaponManager.CurrentWeapon.OnFire += OnWeaponFire;

        Debug.Log(enemy.name + " is engaging " + target.name);
    }

    public override void Tick(float deltaTime)
    {
        if (enemy.AI.Sight.HasLineOfight(target))
        {
            targetPosition = target.Position;

        }
        else if (targetPosition != Vector3.zero)
        {
            Debug.Log("Lost line of sight");
            enemy.AI.SetNewState(new AIS_Investigating(enemy, targetPosition));
        }
        else
        {
            enemy.WeaponManager.CurrentWeapon.TriggerPulled = false;
            enemy.AI.ResetToStartingState();
        }

        //Debug.Log("distanceToTarget = " + distanceToTarget.ToString("F2") + ", enemy.EngagmentRange = " + enemy.EngagmentRange);
        if (distanceToTarget > enemy.EngagmentRange)
        {
            if (enemy.NavMeshAgent.destination != target.Position)
                enemy.NavMeshAgent.SetDestination(target.Position);
        }
        else if (enemy.State.GetType() != Character.AimingStateType)
        {
            //Debug.Log("Stopping");
            enemy.NavMeshAgent.isStopped = true;
            enemy.NavMeshAgent.velocity = Vector3.zero;
            enemy.SetNewState(new CS_Aiming(enemy));
        }
        else if (enemy.State.GetType() == Character.AimingStateType && enemy.FinishedAnimationTranistion)
        {
            //Debug.Log("Aiming Ready to fire " + (Time.time - lastBustTime).ToString("F2"));
            if (Time.time - lastBustTime > enemy.BurstRate)
            {
                if (burstIndex < enemy.BurstAmount)
                {
                    enemy.WeaponManager.CurrentWeapon.TriggerPulled = true;
                    //burstIndex++;
                }
                else if (burstIndex == enemy.BurstAmount)
                {
                    enemy.WeaponManager.CurrentWeapon.TriggerPulled = false;
                    burstIndex = 0;
                    lastBustTime = Time.time;
                }
            }

            if (!target.Health.IsAlive)
            {
                enemy.WeaponManager.CurrentWeapon.TriggerPulled = false;
                enemy.AI.ResetToStartingState();
            }

            
        }


    }

    public override void StateEnd()
    {
        if (enemy.StatusIndicator != null) enemy.StatusIndicator.Hide();
        enemy.AI.ClearTarget();
        enemy.WeaponManager.CurrentWeapon.OnFire -= OnWeaponFire;
        enemy.WeaponManager.CurrentWeapon.TriggerPulled = false;
    }

    private float distanceToTarget
    {
        get
        {
            return Vector3.Distance(enemy.Position, target.Position);
        }

    }

    private void OnWeaponFire()
    {
        burstIndex++;
        //Debug.Log("burstIndex = " + burstIndex + ", time since last bust = " + (Time.time - lastBustTime).ToString());
    }
}
