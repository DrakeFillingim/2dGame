using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : State
{
    public const float RunSpeed = 15;
    private const float SlideWindow = 0.15f;

    private Timer _canSlide = Timer.CreateTimer(_player, () => _controller.AddStateToQueue(new StateQueueData(new WalkState(), 0)), SlideWindow, repeatable: false);

    public override void OnStart()
    {
        _stats.MovementSpeed = RunSpeed;
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

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        _canSlide.StartTimer();
    }
}
