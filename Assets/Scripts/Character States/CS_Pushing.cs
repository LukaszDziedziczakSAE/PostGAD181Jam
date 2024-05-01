using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class CS_Pushing : CharacterState
{
    private InputReader playerInput;
    private Player player;
    private AI ai;
    private readonly int Pushing = Animator.StringToHash("Pushing");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    private Crate crate;


    public CS_Pushing(Character character, Crate crate) : base(character)
    {
        if (character.TryCast<Player>(out Player player))
        {
            this.player = player;
            playerInput = player.Input;
        }
        /*else if (character.TryCast<Enemy>(out Enemy enemy))
        {
            ai = enemy.AI;
        }*/
        this.crate = crate;

        //Debug.Log(character.name + " entered pushing state");
    }

    public override void StateStart()
    {
        character.Animator.CrossFadeInFixedTime(Pushing, CrossFadeDuration);

        if (playerInput == null) Debug.LogError("Missing player input");

        character.transform.eulerAngles = new Vector3 (character.transform.eulerAngles.x, crate.PlayerFaceDirection, character.transform.eulerAngles.z);

        Vector3 position = character.transform.position;
        if (crate.AxisOfMovement == Crate.EAxisOfMovement.zAxis)
        {
            position.z = crate.PlayerPosition;
        }
        else if (crate.AxisOfMovement == Crate.EAxisOfMovement.xAxis)
        {
            position.x = crate.PlayerPosition;
        }

        //Debug.Log("crate position = " + crate.transform.position + ", setting position = " + position);
        character.transform.position = position;
    }

    public override void Tick(float deltaTime)
    {
        if (!character.Health.IsAlive) return;

        if (player != null)
        {
            character.Animator.SetFloat("forward", playerInput.Movement.y);
            character.Animator.SetFloat("right", playerInput.Movement.x);
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


        if (playerInput != null)
        {
            Game.CameraController.Rotate(-playerInput.Look.x);
        }

        if (playerInput.Movement.y < 0 || playerInput.Movement.x != 0)
        {
            character.SetNewState(new CS_Locomotion(character));
        }

        if (movement.magnitude > 0 && playerInput != null)
        {
            //Move(deltaTime);

            crate.Move(playerInput.Movement.y * character.PushingSpeed * deltaTime);

            Vector3 position = character.transform.position;
            Vector3 forward = player.transform.forward;
            position += forward * playerInput.Movement.y * character.PushingSpeed * deltaTime;
            character.Rigidbody.MovePosition(position);
        }
    }

    public override void FixedTick(float deltaTime)
    {
        
    }

    public override void StateEnd()
    {
        //Debug.Log(character.name + " exiting pushing state");
    }

    private void Move(float deltaTime)
    {
        float speed = character.PushingSpeed;
        Vector3 position = character.transform.position;
        //position.x += movement.x * speed * deltaTime;
        //position.z += movement.y * speed * deltaTime;

        Vector3 forward = player.CamFoward - player.transform.position;
        Vector3 right = player.CamRight - player.transform.position;
        position += forward * playerInput.Movement.y * speed * deltaTime
            /*+ right * playerInput.Movement.x * speed * deltaTime*/;
        //FaceDirection(forward, deltaTime);
        character.Rigidbody.MovePosition(position);
    }

    /*private void FaceDirection(Vector3 target, float deltaTime)
    {
        character.transform.rotation = Quaternion.Lerp(
            character.transform.rotation,
            Quaternion.LookRotation(target),
        deltaTime * (isRunning ? character.RotationDamping * 2 : character.RotationDamping));
    }*/

    private void FaceDirection()
    {

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
