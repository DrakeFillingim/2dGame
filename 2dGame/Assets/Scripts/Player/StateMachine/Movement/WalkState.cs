using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : State
{
    private const float WalkSpeed = 3;

    private float _inputDirection = 0;

    public override void OnStart()
    {
        _inputDirection = _inputMap["Move"].ReadValue<float>();

        _stats.MovementSpeed = WalkSpeed;
    }

    public override void OnUpdate()
    {
        Debug.Log("in walk");
    }

    public override void OnFixedUpdate()
    {
        MovePlayer();
        ResetVelocity();
    }

    public override void OnExit()
    {

    }

    protected override void OnMove(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<float>();
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("light attack added to q");
    }

    protected override void OnChargeAttackStarted(InputAction.CallbackContext context)
    {
        Debug.Log("charge attack added to q");
    }

    private void ResetVelocity()
    {
        if (_rb.velocity.magnitude <= 0.01f && !_inputMap["Move"].IsPressed())
        {
            _rb.velocity = Vector2.zero;
            _controller.AddStateToQueue(new StateQueueData(new IdleState(), 0));
        }
    }

    private void MovePlayer()
    {
        float targetSpeed = _inputDirection * _stats.MovementSpeed;
        float speedDifference = (targetSpeed - _rb.velocity.x) / Time.fixedDeltaTime;
        float coefficient = (targetSpeed != 0) ? PlayerStats.Acceleration : PlayerStats.Deceleration;
        float movement = coefficient * speedDifference;
        _rb.AddForce(Vector2.right * movement, ForceMode2D.Force);
    }
}
