using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Statemachine that is the brains of the enemies, running what behaviours they are performing.
/// Each behaviour is a AI_State class
/// </summary>
public abstract class AI_State
{
    protected Enemy enemy; // referance of the enemy character

    public AI_State(Enemy enemy)
    {
        this.enemy = enemy;
    }

    /// <summary>
    /// What happens at the start of the behaviour
    /// </summary>
    public abstract void StateStart();

    /// <summary>
    /// The "update" of the behaviour. What happens every frame.
    /// </summary>
    public abstract void Tick(float deltaTime);

    /// <summary>
    /// What happens at the end of the behaviour
    /// </summary>
    public abstract void StateEnd();
}
