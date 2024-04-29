using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CS_Sneaking : CharacterState
{
    private InputReader playerInput;
    private Player player;
    private AI ai;
    private readonly int Locomotion_Unarmed = Animator.StringToHash("Locomotion_Unarmed");
    private readonly int Locomotion_Crouched = Animator.StringToHash("Locomotion_Crouched");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;


    public CS_Sneaking(Character character) : base(character)
    {
        if (character.TryCast<Player>(out Player player))
        {
            this.player = player;
            playerInput = player.Input;
        }
        else if (character.TryCast<Enemy>(out Enemy enemy))
        {
            ai = enemy.AI;
        }

    }

    public override void StateStart()
    {
        character.Animator.CrossFadeInFixedTime(Locomotion_Crouched, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (playerInput != null && playerInput.Movement.magnitude > 0)
        {
            character.Animator.SetFloat("forward", playerInput.Movement.y);
            character.Animator.SetFloat("right", playerInput.Movement.x);

        }
        else
        {
            character.Animator.SetFloat("forward", 0);
            character.Animator.SetFloat("right", 0);
        }

        if (playerInput != null && !playerInput.Sneaking)
        {
            character.SetNewState(new CS_Locomotion(character));
        }
    }

    public override void FixedTick(float deltaTime)
    {
        if (movement.magnitude > 0 && playerInput != null)
        {
            Move(deltaTime);
        }

        if (playerInput != null)
        {
            Game.CameraController.Rotate(-playerInput.Look.x);
        }
    }

    public override void StateEnd()
    {

    }

    private void Move(float deltaTime)
    {
        float speed = isSneaking ? character.SneakSpeed : isRunning ? character.RunSpeed : character.WalkSpeed;
        Vector3 position = character.transform.position;
        //position.x += movement.x * speed * deltaTime;
        //position.z += movement.y * speed * deltaTime;

        Vector3 forward = player.CamFoward - player.transform.position;
        Vector3 right = player.CamRight - player.transform.position;
        position += forward * playerInput.Movement.y * speed * deltaTime + right * playerInput.Movement.x * speed * deltaTime;
        FaceDirection(forward, deltaTime);
        character.Rigidbody.MovePosition(position);
    }

    private void FaceDirection(Vector3 target, float deltaTime)
    {
        character.transform.rotation = Quaternion.Lerp(
            character.transform.rotation,
            Quaternion.LookRotation(target),
            deltaTime * (isRunning ? character.RotationDamping * 2 : character.RotationDamping));
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

    private bool isSneaking
    {
        get
        {
            if (playerInput != null) return playerInput.Sneaking;

            else return false;
        }
    }
}
