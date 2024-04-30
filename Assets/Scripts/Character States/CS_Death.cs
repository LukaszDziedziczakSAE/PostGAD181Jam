using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Death : CharacterState
{
    private readonly int Dying = Animator.StringToHash("Dying");
    private const float CrossFadeDuration = 0.1f;

    public CS_Death(Character character) : base(character)
    {
    }

    public override void StateStart()
    {
        character.Animator.CrossFadeInFixedTime(Dying, CrossFadeDuration);
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
