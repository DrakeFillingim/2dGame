using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : State
{
    public const float RunSpeed = 12.5f;
    private const float SlideWindow = 0.2f;

    private Timer _canSlide = Timer.CreateTimer(_player, "CanSlideTimer", () => _controller.AddStateToQueue(new StateQueueData(new WalkState(), 0)), SlideWindow, repeatable: false);

    public override void OnStart()
    {
        _stats.MovementSpeed = RunSpeed;
        if (!_inputMap["Run"].IsPressed())
        {
            _controller.AddStateToQueue(new StateQueueData(new WalkState()));
        }
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
        if (_canSlide != null)
        {
            Object.Destroy(_canSlide);
        }
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
        _controller.AddStateToQueue(new StateQueueData(new SlideState()));
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        _canSlide.StartTimer();
    }
}
