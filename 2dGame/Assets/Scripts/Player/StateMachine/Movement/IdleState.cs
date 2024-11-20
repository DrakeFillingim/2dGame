using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : State
{
    public override void OnStart()
    {
        if (_inputMap["ChargeAttack"].IsInProgress())
        {
            _controller.AddStateToQueue(new StateQueueData(new ChargeAttackState()));
        }
        if (_inputMap["Move"].IsPressed())
        {
            if (_inputMap["Run"].inProgress)
            {
                _controller.AddStateToQueue(new StateQueueData(new RunState()));
            }
            else
            {
                _controller.AddStateToQueue(new StateQueueData(new WalkState()));
            }
        }
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        if (!MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new FallState(), destructable: true));
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

    protected override void OnMove(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() != 0)
        {
            if (_inputMap["Run"].inProgress)
            {
                _controller.AddStateToQueue(new StateQueueData(new RunState()));
            }
            else
            {
                _controller.AddStateToQueue(new StateQueueData(new WalkState()));
            }
        }
    }

    protected override void OnCrouch(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new CrouchState()));
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new LightAttackState()));
    }

    protected override void OnChargeAttackStarted(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new ChargeAttackState()));
    }
}
