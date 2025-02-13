using UnityEngine;

public class EnemyAttack : EnemyState
{
    private AttackComponent _attackComponent;
    private int _animationHash;

    private bool canTransition = true;

    private const float AttackTime = .4f;
    private float _currentAttackTime = 0;

    public EnemyAttack(GameObject agent, string attackName)
    {
        _animationHash = Animator.StringToHash(attackName);
        _attackComponent = agent.GetComponent<AttackComponent>();

        agent.GetComponent<SensorLayer>().PlayerAttack += OnPlayerAttack;
        agent.GetComponent<EntityStats>().Parried += OnPlayerParry;
    }

    public override void OnEnter()
    {
        _attackComponent.StartAttack(_animationHash);
    }

    public override void OnUpdate()
    {
        Debug.Log("in attack");
        _currentAttackTime += Time.deltaTime;
        if (_currentAttackTime > AttackTime)
        {
            _onStateFinish?.Invoke();
        }
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnExit()
    {
        _currentAttackTime = 0;
    }

    protected override void OnPlayerAttack()
    {
        Debug.Log("player attack");
        if (canTransition)
        {
            _onPlayerAttack?.Invoke();
        }
    }

    protected override void OnTakeDamage()
    {
        
    }

    protected override void OnPlayerParry()
    {
        _onPlayerParry?.Invoke();
    }
}
