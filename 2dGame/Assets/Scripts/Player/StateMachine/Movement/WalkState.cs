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
            _controller.AddStateToQueue(new StateQueueData(new IdleState(), 0));
        }
        if (!MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new FallState(), 0));
        }
    }

    public override void OnExit()
    {

    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState(), 0));
    }

    protected override void OnCrouch(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new CrouchState(), 0));
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new RunState(), 0));
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("light attack added to q");
    }

    protected override void OnChargeAttackStarted(InputAction.CallbackContext context)
    {
        Debug.Log("charge attack added to q");
    }
}
