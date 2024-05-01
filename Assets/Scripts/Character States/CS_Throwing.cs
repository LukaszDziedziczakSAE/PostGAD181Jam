using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Throwing : CharacterState
{
    private readonly int Throw = Animator.StringToHash("Throw");
    private const float CrossFadeDuration = 0.1f;

    private BottleThrower bottleThrower;

    public CS_Throwing(Character character) : base(character)
    {
        bottleThrower = character.GetComponent<BottleThrower>();
    }

    public override void StateStart()
    {
        //Time.timeScale = 0.1f;
        character.Animator.CrossFadeInFixedTime(Throw, CrossFadeDuration);
        bottleThrower.SpawnBottle();  
    }

    public override void Tick(float deltaTime)
    {
        if (character.Animator.GetCurrentAnimatorStateInfo(0).IsTag("Throw") 
            && character.Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            character.SetNewState(new CS_Locomotion(character));
        }
    }

    public override void FixedTick(float deltaTime)
    {

    }

    public override void StateEnd()
    {

    }
}
