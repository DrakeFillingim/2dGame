using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : State
{
    public const float WalkSpeed = 5;

    public override void OnStart()
    {
        _stats.MovementSpeed = WalkSpeed;
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        if (_rb.velocity == Vector2.zero && !_inputMap["Move"].IsPressed())
        {
            _controller.AddStateToQueue(new StateQueueData(new IdleState()));
        }
        if (!MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new FallState()));
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

    protected override void OnCrouch(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new CrouchState()));
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new RunState()));
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        //Debug.Log("light attack added to q");
    }

    protected override void OnChargeAttackStarted(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new ChargeAttackState()));
    }
}
