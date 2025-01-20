using UnityEngine;

public class EnemyAttack : EnemyState
{
    private Animator _animator;
    private int _animationHash;

    private bool canTransition = true;

    public EnemyAttack(GameObject agent, string attackName)
    {
        _animator = agent.GetComponent<Animator>();
        _animationHash = Animator.StringToHash(attackName);

        agent.GetComponent<SensorLayer>().PlayerAttack += OnPlayerAttack;
    }

    public override void OnEnter()
    {
        _animator.Play(_animationHash);
    }

    public override void OnUpdate()
    {
        Debug.Log("in attack");
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnExit()
    {

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
}
