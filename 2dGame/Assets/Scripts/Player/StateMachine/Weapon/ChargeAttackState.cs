using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChargeAttackState : State
{
    public const float ChargeWalkSpeed = 1.5f;
    private const float ChargeDistance = 6.25f;
    private const float ChargeTime = .5f;
    private float _currentChargeTime = 0;

    private int _direction = 1;
    private bool _performed = false;

    private static PlayerMovement _movement = _player.GetComponent<PlayerMovement>();

    private float startX;

    public override void OnStart()
    {
        _stats.MovementSpeed = ChargeWalkSpeed;
        _inputMap["ChargeAttack"].performed += OnChargeAttackPerformed;
        _inputMap["ChargeAttack"].canceled += OnChargeAttackCanceled;
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnFixedUpdate()
    {
        if (_performed)
        {
            if (_currentChargeTime >= ChargeTime)
            {
                _controller.AddStateToQueue(new StateQueueData(new IdleState()));
                _rb.velocity = Vector2.zero;
                return;
            }
            _rb.velocity = _direction * new Vector2(ChargeDistance * DerivateCalculator.FivePointDerive(Easings.EaseOutExpo, _currentChargeTime, ChargeTime), 0);
            _currentChargeTime += Time.deltaTime;
        }
    }

    public override void OnExit()
    {
        Time.fixedDeltaTime = 0.02f;
        Debug.Log("distance traveled: " + (startX - _player.transform.position.x));
        _inputMap["Move"].Enable();
        _movement.CheckMoveInput();

        _stats.MovementSpeed = WalkState.WalkSpeed;
        _stats.ApplyFriction = true;

        _inputMap["ChargeAttack"].performed -= OnChargeAttackPerformed;
        _inputMap["ChargeAttack"].canceled -= OnChargeAttackCanceled;
    }

    private void OnChargeAttackPerformed(InputAction.CallbackContext context)
    {
        Time.fixedDeltaTime = 0.001f;
        startX = _player.transform.position.x;
        _inputMap["Move"].Disable();
        _movement.CheckMoveInput();
        _stats.ApplyFriction = false;

        _performed = true;
        if (_renderer.flipX)
        {
            _direction = -1;
        }
    }

    private void OnChargeAttackCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("canceled");
        _controller.AddStateToQueue(new StateQueueData(new LightAttackState()));
    }
}
