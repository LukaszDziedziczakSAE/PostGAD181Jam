using System;
using System.Collections.Generic;
using UnityEngine;

public class CS_Locomotion : CharacterState
{
    private InputReader playerInput;
    private Player player;
    private AI ai;
    private readonly int Locomotion_Unarmed = Animator.StringToHash("Locomotion_Unarmed");
    private readonly int Locomotion_Rifle = Animator.StringToHash("Locomotion_Rifle");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    private const float PositionCheckSphereCastSize = 0.1f;
    private Vector3 lastPosition;
    private Collider groundColider;

    public CS_Locomotion(Character character) : base(character)
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
        if (character.WeaponManager != null)
        {
            if (character.WeaponManager.WeaponSpwaned)
            {
                character.Animator.CrossFadeInFixedTime(Locomotion_Rifle, CrossFadeDuration);
            }
            else
            {
                character.Animator.CrossFadeInFixedTime(Locomotion_Unarmed, CrossFadeDuration);
            }
        }
        else character.Animator.CrossFadeInFixedTime(Locomotion_Unarmed, CrossFadeDuration);

        //if (playerInput == null) Debug.LogError("Missing player input");

        if (playerInput != null)
        {
            playerInput.OnAttackPress += PlayerInput_OnAttackPress;
            SetGroundColider();
        }
        
    }

    private void PlayerInput_OnAttackPress()
    {
        if (player.Inventory.HasBottles) player.SetNewState(new CS_Throwing(player));

    }

    public override void Tick(float deltaTime)
    {
        lastPosition = character.transform.position;

        if (!character.Health.IsAlive) return;

        if (player !=null)
        {
            character.Animator.SetFloat("forward", isRunning ? playerInput.Movement.y * 2 : playerInput.Movement.y);
            character.Animator.SetFloat("right", isRunning ? playerInput.Movement.x * 2 : playerInput.Movement.x);
        }

        else
        {
            // drive movement and animation
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

        

        if (playerInput != null && playerInput.Sneaking)
        {
            character.SetNewState(new CS_Sneaking(character));
        }

        if (playerInput != null)
        {
            Game.CameraController.Rotate(-playerInput.Look.x);
        }

        Vector3 rotation = character.transform.eulerAngles;
        rotation.z = 0;
        rotation.x = 0;
        character.transform.eulerAngles = rotation;


    }

    public override void FixedTick(float deltaTime)
    {
        if (movement.magnitude > 0 && playerInput != null)
        {
            Move(deltaTime);
        }

        
    }

    public override void StateEnd()
    {
        if (playerInput != null)
        {
            playerInput.OnAttackPress -= PlayerInput_OnAttackPress;
        }
    }

    private void Move(float deltaTime)
    {
        float speed = isSneaking ? character.SneakSpeed : isRunning ? character.RunSpeed : character.WalkSpeed;
        Vector3 position = character.transform.position;

        Vector3 forward = player.CamFoward - player.transform.position;
        Vector3 right = player.CamRight - player.transform.position;

        position += forward * playerInput.Movement.y * speed * deltaTime 
            + right * playerInput.Movement.x * speed * deltaTime;
        FaceDirection(forward, deltaTime);
        position = CorrectGroundPosition(position);
        if (isRunning && CanMoveToPosition(position)) character.Rigidbody.MovePosition(position);
        else if (!isRunning) character.Rigidbody.MovePosition(position);
    }

    private void FaceDirection(Vector3 target, float deltaTime)
    {
        character.transform.rotation = Quaternion.Lerp(
            character.transform.rotation,
            Quaternion.LookRotation(target),
            deltaTime * (isRunning ? character.RotationDamping * 2 : character.RotationDamping));

        character.transform.eulerAngles = new Vector3(0, character.transform.eulerAngles.y, 0);
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

    private Vector3 CorrectGroundPosition(Vector3 position)
    {
        Vector3 newPosition = position;
        Vector3 rayCastOrigin = new Vector3(position.x, position.y + Game.GroundRaycastHeight, position.z);
        Vector3 raycastDirection = new Vector3(0, -1, 0);
        //Debug.DrawLine(rayCastOrigin, rayCastOrigin + (raycastDirection * Game.GroundRaycastHeight * 2), Color.red);
        //Debug.Log("raycast origin = " + rayCastOrigin);
        if (Physics.Raycast(rayCastOrigin, raycastDirection, out RaycastHit hit, Game.GroundRaycastHeight * 2, Game.GroundLayers) &&
            Vector3.Distance(newPosition, hit.point) < Game.MinFallHeight)
        {
            newPosition = hit.point;
            groundColider = hit.collider;
        }
        else
        {
            //Debug.LogWarning("Ground raycast failed to hit ground");
            newPosition.y = lastPosition.y;
        }

        return newPosition;
    }

    private bool CanMoveToPosition(Vector3 postion)
    {
        RaycastHit[] hits = Physics.SphereCastAll(postion, PositionCheckSphereCastSize, character.transform.up);

        List<RaycastHit> validHits = new List<RaycastHit>();

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider == character.CapsuleCollider ||
                hit.collider == groundColider ||
                hit.collider.tag == "DoNotHit") continue;
            validHits.Add(hit);
        }

        if (validHits.Count > 0)
        {
            foreach(RaycastHit validHit in validHits)
            {
                Debug.Log("Blocking colider " + validHit.collider.name);
            }
            return false;
        }
        else return true;
    }

    private void SetGroundColider()
    {
        if (Physics.Raycast(character.transform.position, new Vector3(0, -1, 0), out RaycastHit hit, Game.GroundRaycastHeight, Game.GroundLayers))
        {
            groundColider = hit.collider;
        }
    }
}