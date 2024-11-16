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
                _controller.AddStateToQueue(new StateQueueData(new IdleState()));
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

        _inputMap["ChargeAttack"].canceled -= OnChargeAttackCanceled;
    }

    private void OnChargeAttackPerformed()
    {
        _startX = _player.transform.position.x;
        _startY = _player.transform.position.y;
        _inputMap["Move"].Disable();
        _movement.CheckMoveInput();

        _performed = true;
        if (_renderer.flipX)
        {
            _direction = -1;
        }
    }

    private void OnChargeAttackCanceled(InputAction.CallbackContext context)
    {
        if (!_performed)
        {
            _controller.AddStateToQueue(new StateQueueData(new LightAttackState()));
        }
    }
}
