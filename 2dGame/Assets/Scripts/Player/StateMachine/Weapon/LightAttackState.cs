using UnityEngine;
using UnityEngine.InputSystem;

public class LightAttackState : State
{
    //replace with animation timing?
    private static float attackTime = 1f;
    private float _currentAttackTime = 0;

    public override void OnStart()
    {
        _playerAnimator.Play("Attack");
        _playerAnimator.Update(0);
        attackTime = _playerAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
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
    }
}