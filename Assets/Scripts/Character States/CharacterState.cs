public abstract class CharacterState
{
    protected Character character;

    public CharacterState(Character character)
    {
        this.character = character;
    }

    public abstract void StateStart();
    public abstract void Tick(float deltaTime);
    public abstract void FixedTick(float deltaTime);
    public abstract void StateEnd();
}
