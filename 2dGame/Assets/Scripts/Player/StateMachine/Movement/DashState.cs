using UnityEngine;
using UnityEngine.InputSystem;

public class DashState : State
{
    private const float DashDistance = 6.25f;
    private const float DashTime = .75f;
    private float _currentDashTime = 0;
    private int _direction = 1;
    private Vector2 _startPosition = Vector2.zero;

    private static PlayerMovement _movement = _player.GetComponent<PlayerMovement>();

    private Vector2 _previousFrameMotion = Vector2.zero;

    public override void OnStart()
    {
        _inputMap["Move"].Disable();
        _movement.CheckMoveInput();

        if(_renderer.flipX)
        {
            _direction = -1;
        }
        _startPosition = _player.transform.position;

        _rb.velocity = Vector2.zero;
        _stats.ApplyGravity = false;
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        Vector2 deltaPosition = new (_direction * DashDistance * Easings.EaseOutQuint(_currentDashTime, DashTime), 0);
        _rb.MovePosition(_startPosition + deltaPosition);
        _currentDashTime += Time.fixedDeltaTime;
        if (((deltaPosition - _previousFrameMotion).magnitude < .1 && _currentDashTime > (DashTime / 2)) || _currentDashTime >= DashTime)
        {
            Debug.Log("from inside dash");
            if (MovementHelper.IsGrounded(_player, _stats.GravityDirection))
            {
                _controller.AddStateToQueue(new StateQueueData(new IdleState()));
            }
            else
            {
                _controller.AddStateToQueue(new StateQueueData(new FallState()));
            }
        }

        _previousFrameMotion = deltaPosition;
    }

    public override void OnExit()
    {
        _inputMap["Move"].Enable();
        _movement.CheckMoveInput();

        _stats.ApplyGravity = true;
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState(), .5f));
    }

    protected override void OnDash(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new DashState(), .25f));
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new MovementAttackState(), .5f));
    }
}
