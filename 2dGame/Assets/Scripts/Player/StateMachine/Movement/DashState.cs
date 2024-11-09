using UnityEngine;
using UnityEngine.InputSystem;

public class DashState : State
{
    private const float DashDistance = 6.25f;
    private const float DashTime = .75f;
    private float _currentDashTime = 0;
    private int _direction = 1;

    private static PlayerMovement _movement = _player.GetComponent<PlayerMovement>();

    public override void OnStart()
    {
        _inputMap["Move"].Disable();
        _movement.CheckMoveInput();

        if(_renderer.flipX)
        {
            _direction = -1;
        }

        _rb.velocity = Vector2.zero;
        _stats.ApplyGravity = false;
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        _rb.velocity = _direction * new Vector2(DashDistance * DerivateCalculator.ThreePointDerive(Easings.EaseOutQuint, _currentDashTime, DashTime), 0);
        _currentDashTime += Time.fixedDeltaTime;
        if ((Mathf.Abs(_rb.velocity.x) <= 5 && _currentDashTime >= DashTime / 3) || _currentDashTime >= DashTime)
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
}
