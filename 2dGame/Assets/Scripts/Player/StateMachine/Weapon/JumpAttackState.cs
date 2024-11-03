using UnityEngine;
using UnityEngine.InputSystem;

public class JumpAttackState : State
{
    private const float _attackLength = .25f;
    private Timer _attackTimer = Timer.CreateTimer(_player, OnAttackEnd, _attackLength, repeatable: false);

    public override void OnStart()
    {
        Debug.Log("attack staterd");
        _renderer.color = Color.red;
        _attackTimer.StartTimer();
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        Debug.Log("attack finished");
        _renderer.color = Color.white;
    }

    private static void OnAttackEnd()
    {
        if (MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new IdleState(), 0));
            return;
        }
        _controller.AddStateToQueue(new StateQueueData(new FallState(), 0));
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState(), .3f));
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        _stats.MovementSpeed = RunState.RunSpeed;
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        _stats.MovementSpeed = WalkState.WalkSpeed;
    }
}
