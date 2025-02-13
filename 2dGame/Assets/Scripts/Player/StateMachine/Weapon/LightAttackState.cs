using UnityEngine;

public class LightAttackState : State
{
    //replace with animation timing?
    private static float attackTime = .4f;
    private float _currentAttackTime = 0;

    public override void OnStart()
    {
        _player.GetComponent<AttackComponent>().StartAttack(Animator.StringToHash("Attack"));
        _renderer.color = Color.red;
    }

    public override void OnUpdate()
    {
        _currentAttackTime += Time.deltaTime;
        if (_currentAttackTime >= attackTime)
        {
            _controller.AddStateToQueue(new StateQueueData(new IdleState()));
        }
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnExit()
    {
        _renderer.color = Color.white;
        _currentAttackTime = 0;
    }
}