using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Aiming : CharacterState
{
    private InputReader playerInput;
    private AI ai;
    private readonly int RifleDownToAim = Animator.StringToHash("RifleDownToAim");
    private readonly int RifleAimToDown = Animator.StringToHash("RifleAimToDown");
    private readonly int RifleFire = Animator.StringToHash("RifleFire");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public CS_Aiming(Character character) : base(character)
    {
        if (character.TryCast<Player>(out Player player))
        {
            playerInput = player.Input;
        }
        else if (character.TryCast<Enemy>(out Enemy enemy))
        {
            ai = enemy.AI;
        }
    }

    public override void StateStart()
    {
        character.WeaponManager.CurrentWeapon.OnFire += OnWeaponFired;
        if (character.WeaponManager.WeaponSpwaned)
        {
            character.Animator.CrossFadeInFixedTime(RifleDownToAim, CrossFadeDuration);
        }

    }

    public override void Tick(float deltaTime)
    {
        if (movement.magnitude > 0)
        {
            //Move(deltaTime);
            character.Animator.SetFloat("forward", isRunning ? 2f : 1f);
        }
        else
        {
            character.Animator.SetFloat("forward", 0);
        }
    }

    public override void FixedTick(float deltaTime)
    {
        if (ai != null && ai.Target != null)
        {
            FaceDirection(deltaTime);
        }
    }

    public override void StateEnd()
    {
        character.WeaponManager.CurrentWeapon.OnFire -= OnWeaponFired;
        if (character.WeaponManager.WeaponSpwaned)
        {
            character.Animator.CrossFadeInFixedTime(RifleAimToDown, CrossFadeDuration);
        }
    }

    private Vector2 movement
    {
        get
        {
            if (playerInput != null) return playerInput.Movement;
            else if (ai != null)
            {
                if (ai.Enemy.NavMeshAgent.isStopped)
                {
                    return Vector2.zero;
                }
                else return ai.Enemy.NavMeshAgent.velocity;
            }
            else return Vector2.zero;
        }
    }
    private bool isRunning
    {
        get
        {
            if (playerInput != null) return playerInput.Running;
            else if (ai != null)
            {
                if (ai.Enemy.NavMeshAgent.speed == ai.Enemy.RunSpeed)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }

    private void FaceDirection(float deltaTime)
    {
        Vector3 direction =  ai.Target.Position- ai.Enemy.Position;
        direction = direction.normalized;
        Vector3 target = new Vector3(direction.x, 0, direction.z);

        character.transform.rotation = Quaternion.Lerp(
            character.transform.rotation,
            Quaternion.LookRotation(target),
            deltaTime * (isRunning ? character.RotationDamping * 2 : character.RotationDamping));
    }

    private void OnWeaponFired()
    {
        character.Animator.CrossFadeInFixedTime(RifleFire, CrossFadeDuration);
    }

    protected new float GetNormalizedTime(Animator animator)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else return 0;
    }

    
}
