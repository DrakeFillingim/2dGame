using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : State
{
    public override void OnStart()
    {
        if (_inputMap["Move"].IsPressed())
        {
            _controller.AddStateToQueue(new StateQueueData(new WalkState(), 0));
        }
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
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

    protected override void OnMove(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() != 0)
        {
            _controller.AddStateToQueue(new StateQueueData(new WalkState(), 0));
        }
    }

    protected override void OnCrouch(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new CrouchState(), 0));
    }
}
