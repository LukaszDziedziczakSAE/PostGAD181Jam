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

        if (playerInput == null) Debug.LogError("Missing player input");
    }

    public override void Tick(float deltaTime)
    {
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

    }

    private void Move(float deltaTime)
    {
        float speed = isSneaking ? character.SneakSpeed : isRunning ? character.RunSpeed : character.WalkSpeed;
        Vector3 position = character.transform.position;
        //position.x += movement.x * speed * deltaTime;
        //position.z += movement.y * speed * deltaTime;

        Vector3 forward = player.CamFoward - player.transform.position;
        Vector3 right = player.CamRight - player.transform.position;
        position += forward * playerInput.Movement.y * speed * deltaTime 
            + right * playerInput.Movement.x * speed * deltaTime;
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