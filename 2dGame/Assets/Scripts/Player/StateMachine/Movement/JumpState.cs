using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : State
{
    private const float JumpHeight = 6;
    private const float JumpGravity = 30;
    private static float JumpForce = Mathf.Sqrt(2 * JumpGravity * JumpHeight);

    private float _startingGravity;

    public override void OnStart()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(-_stats.GravityDirection * JumpForce, ForceMode2D.Impulse);
        _startingGravity = _stats.GravityScale;
        _stats.GravityScale = JumpGravity;
    }

    public override void OnUpdate()
    {

    }

    //set fall after jump anim completed?
    public override void OnFixedUpdate()
    {
        if (_rb.velocity.y <= 5)
        {
            _controller.AddStateToQueue(new StateQueueData(new FallState(), destructable: true));
        }
    }

    public override void OnExit()
    {
        _stats.GravityScale = _startingGravity;
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState()));
    }

    protected override void OnDash(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new DashState()));
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpAttackState(), .75f));
    }
}
