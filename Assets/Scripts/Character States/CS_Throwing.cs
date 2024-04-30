using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Throwing : CharacterState
{
    private readonly int Throw = Animator.StringToHash("Throw");
    private const float CrossFadeDuration = 0.1f;

    public CS_Throwing(Character character) : base(character)
    {
        character.Animator.CrossFadeInFixedTime(Throw, CrossFadeDuration);
    }

    public override void StateStart()
    {

    }

    public override void Tick(float deltaTime)
    {

    }

    public override void FixedTick(float deltaTime)
    {

    }

    public override void StateEnd()
    {

    }
}
