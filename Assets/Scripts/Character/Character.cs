using UnityEngine;

public class Character : MonoBehaviour
{
    [field: SerializeField, Header("Referances")] public Rigidbody Rigidbody { get; private set; }
    public CharacterState State { get; private set; }
    [field: SerializeField] public CapsuleCollider CapsuleCollider { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public WeaponManager WeaponManager { get; private set; }
    [field: SerializeField] public Transform LeftHand { get; private set; }
    [field: SerializeField] public Transform RightHand { get; private set; }
    [field: SerializeField] public SFX_Footstep SFX_Footstep { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Combat Combat { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public SFX_Body SFX_Body { get; private set; }
    [field: SerializeField] public SFX_Voice SFX_Voice { get; private set; }
    [field: SerializeField] public Footsteps Footsteps { get; private set; }

    [field: SerializeField, Header("Setting")] public float WalkSpeed { get; private set; } = 5f;
    [field: SerializeField] public float RunSpeed { get; private set; } = 10f;
    [field: SerializeField] public float SneakSpeed { get; private set; } = 10f;
    [field: SerializeField] public float RotationDamping { get; private set; } = 0.5f;
    [field: SerializeField] public float PushingSpeed { get; private set; } = 4f;

    public Vector3 Position => transform.position;
    System.Type aimingStateType;

    Vector3 lastHandPositing;
    Vector3 startingHandPosition;

    public Vector3 HandPosition
    {
        get
        {
            return RightHand.position;
        }
    }

    protected virtual void Start()
    {
        startingHandPosition = RightHand.position;
        SetNewState(new CS_Locomotion(this));
    }

    private void Update()
    {
        if (State != null) State.Tick(Time.deltaTime);
        if (RightHand.position != startingHandPosition) lastHandPositing = RightHand.position;

        /*print("RH post = " + RightHand.position + ", distance = " + Vector3.Distance(transform.position, RightHand.position));*/
    }

    private void FixedUpdate()
    {
        if (State != null) State.FixedTick(Time.deltaTime);
    }

    public void SetNewState(CharacterState newState)
    {
        if (State != null) State.StateEnd();
        State = newState;
        State.StateStart();
    }

    public bool TryCast<T>(out T castedObj)
    {
        object character = this;

        if (character is T)
        {
            castedObj = (T)character;
            return true;
        }

        castedObj = default(T);
        return false;
    }

    public static System.Type AimingStateType => typeof(CS_Aiming);

    public bool FinishedAnimationTranistion
    {
        get
        {
            AnimatorStateInfo currentInfo = Animator.GetCurrentAnimatorStateInfo(0);
            AnimatorStateInfo nextInfo = Animator.GetNextAnimatorStateInfo(0);

            if (Animator.IsInTransition(0) /*&& nextInfo.IsTag("")*/)
            {
                return false;
            }
            else if (!Animator.IsInTransition(0) && currentInfo.IsTag("Transition"))
            {
                return false;
            }
            else return true;
        }

    }
}
