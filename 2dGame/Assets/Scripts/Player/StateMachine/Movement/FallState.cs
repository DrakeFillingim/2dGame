using UnityEngine.InputSystem;

public class FallState : State
{
    public override void OnStart()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        if (MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new IdleState(), destructable: true));
        }
    }

    public override void OnExit()
    {

    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState()));
    }

    protected override void OnDash(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new DashState()));
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
        _controller.AddStateToQueue(new StateQueueData(new JumpAttackState()));
    }
}
