using UnityEngine;
using UnityEngine.InputSystem;

public class ChargeAttackState : State
{
    public const float ChargeWalkSpeed = 1.5f;
    private const float ChargeDistance = 6.25f;
    private const float ChargeTime = .4f;
    private float _currentChargeTime = 0;
    private float _startX;
    private float _startY;

    private int _direction = 1;
    private bool _performed = false;

    private const float ChargeHoldTime = 0.8f;
    private float _currentHoldTime = 0;

    private static PlayerMovement _movement = _player.GetComponent<PlayerMovement>();

    public override void OnStart()
    {
        if (!_inputMap["ChargeAttack"].IsInProgress())
        {
            Debug.Log("charge started but no clicky");
            _controller.AddStateToQueue(new StateQueueData(new IdleState()));
        }

        _stats.MovementSpeed = ChargeWalkSpeed;
        _inputMap["ChargeAttack"].canceled += OnChargeAttackCanceled;
    }

    public override void OnUpdate()
    {
        _currentHoldTime += Time.deltaTime;
        if (_currentHoldTime > ChargeHoldTime && !_performed)
        {
            OnChargeAttackPerformed();
        }
    }

    public override void OnFixedUpdate()
    {
        if (_performed)
        {
            _rb.MovePosition(new Vector2(_startX + (_direction * ChargeDistance * Easings.EaseOutExpo(_currentChargeTime, ChargeTime)), _startY));
            if (_currentChargeTime > ChargeTime)
            {
                if (MovementHelper.IsGrounded(_player, _stats.GravityDirection))
                {
                    _controller.AddStateToQueue(new StateQueueData(new IdleState()));
                }
                else
                {
                    _controller.AddStateToQueue(new StateQueueData(new FallState()));
                }
            }
            _currentChargeTime += Time.deltaTime;
        }
    }

    public override void OnExit()
    {
        Debug.Log("distance traveled: " + (_startX - _player.transform.position.x));
        _inputMap["Move"].Enable();
        _movement.CheckMoveInput();
        
        _stats.MovementSpeed = WalkState.WalkSpeed;
        _stats.ApplyGravity = true;

        _inputMap["ChargeAttack"].canceled -= OnChargeAttackCanceled;
    }

    private void OnChargeAttackPerformed()
    {
        _startX = _player.transform.position.x;
        _startY = _player.transform.position.y;
        _inputMap["Move"].Disable();
        _movement.CheckMoveInput();

        _rb.velocity = Vector2.zero;
        _stats.ApplyGravity = false;

        _performed = true;
        _direction = (int)_player.transform.localScale.x;
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState(), .25f));
    }

    protected override void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("charge dash pressed");
        _controller.AddStateToQueue(new StateQueueData(new DashState(), 1f));
    }

    private void OnChargeAttackCanceled(InputAction.CallbackContext context)
    {
        if (!_performed)
        {
            Debug.Log("canceled");
            _controller.AddStateToQueue(new StateQueueData(new LightAttackState()));
        }
    }
}
