using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : State
{
    private float _jumpHeight = 5;

    public override void OnStart()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        Debug.Log(Mathf.Sqrt(2 * _stats.GravityScale * _jumpHeight));
        _rb.AddForce(-_stats.GravityDirection * Mathf.Sqrt(2 * _stats.GravityScale * _jumpHeight), ForceMode2D.Impulse);
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        if (_rb.velocity.y <= 0)
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

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpAttackState(), 2));
    }
}
