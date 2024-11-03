using UnityEngine;
using UnityEngine.InputSystem;

public class FallState : State
{
    public override void OnStart()
    {
        _stats.GravityScale = 45;
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        if (MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new IdleState(), 0));
        }
    }

    public override void OnExit()
    {
        _stats.GravityScale = 30;
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState(), 0));
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        _stats.MovementSpeed = RunState.RunSpeed;
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        _stats.MovementSpeed = WalkState.WalkSpeed;
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpAttackState(), 0));
    }
}
