public abstract class EnemyState
{
    public delegate void StateChangeEvent();

    
    protected StateChangeEvent _onTakeDamage;
    protected StateChangeEvent _onStateFinish;
    protected StateChangeEvent _onPlayerEntersRange;

    protected StateChangeEvent _onPlayerAttack;

    protected StateChangeEvent _onPlayerParry;

    public virtual void OnEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnExit()
    {

    }

    public void ConnectEvents(StateChangeEvent onPlayerAttack = null, StateChangeEvent onTakeDamage = null,
                              StateChangeEvent onStateFinish = null, StateChangeEvent onPlayerEntersRange = null,
                              StateChangeEvent onPlayerParry = null)
    {
        _onPlayerAttack = onPlayerAttack;
        _onTakeDamage = onTakeDamage;
        _onStateFinish = onStateFinish;
        _onPlayerEntersRange = onPlayerEntersRange;
        _onPlayerParry = onPlayerParry;
    }

    protected virtual void OnPlayerAttack()
    {
        
    }

    protected virtual void OnTakeDamage()
    {

    }

    protected virtual void OnPlayerEntersRange()
    {

    }

    protected virtual void OnPlayerParry()
    {

    }
}
