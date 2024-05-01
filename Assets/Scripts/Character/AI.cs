using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [field: SerializeField] public Enemy Enemy;
    public AI_State State {  get; private set; }
    [field: SerializeField] public AI_Sight Sight { get; private set; }
    [field: SerializeField] public AI_Hearing Hearing { get; private set; }

    [field: SerializeField, Header("Debug")] public Vector2 Movement { get; private set; }
    [field: SerializeField] public bool Running { get; private set; }
    [field: SerializeField] public List<Character> CharactersInRange { get; private set; } = new List<Character>();
    [field: SerializeField] public Character Target { get; private set; }

    AI_State lastState;

    private void Start()
    {
        ResetToStartingState();
    }

    public void ResetToStartingState()
    {
        switch (Enemy.StartingMode)
        {
            case Enemy.EMode.Inactive:
                SetNewState(new AIS_Inactive(Enemy));
                break;

            case Enemy.EMode.Patrol:
                SetNewState(new AIS_Patroling(Enemy));
                break;

            case Enemy.EMode.Gaurd:
                SetNewState(new AIS_Gaurding(Enemy));
                break;
        }
    }

    private void Update()
    {
        if (!Enemy.Health.IsAlive) return;
        if (State != null) State.Tick(Time.deltaTime);
    }

    public void SetNewState(AI_State newState)
    {
        if (State != null)
        {
            lastState = State;
            State.StateEnd();
        }
        State = newState;
        State.StateStart();


    }

    public void SetLocomotion(Vector2 movement, bool isRunning = false)
    {
        Movement = movement;
        Running = isRunning;
    }

    public void ClearLocomotion()
    {
        Movement = Vector2.zero;
        Running = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name + " entered enemy radius");

        if (other.TryGetComponent<Character>(out Character character))
        {
            CharactersInRange.Add(character);
            if (character.TryCast<Player>(out Player player))
            {
                player.Footsteps.OnWalkStep += Hearing.OnHearWalkStep;
                player.Footsteps.OnRunStep += Hearing.OnHearRunStep;
                player.SFX_Voice.PlayDaPoliceClips();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            CharactersInRange.Remove(character);
            if (character.TryCast<Player>(out Player player))
            {
                player.Footsteps.OnWalkStep -= Hearing.OnHearWalkStep;
                player.Footsteps.OnRunStep -= Hearing.OnHearRunStep;
            }
        }
    }

    public void SetTarget(Character character)
    {
        Target = character;
    }

    public void ClearTarget()
    {
        Target = null;
    }
}
