using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIS_Inactive : AI_State
{
    public AIS_Inactive(Enemy enemy) : base(enemy)
    {
    }

    public override void StateStart()
    {
        if (enemy.StatusIndicator != null) enemy.StatusIndicator.Hide();
    }

    public override void Tick(float deltaTime)
    {
    }

    public override void StateEnd()
    {
        
    }
}
