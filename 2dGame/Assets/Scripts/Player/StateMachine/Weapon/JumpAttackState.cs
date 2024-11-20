using UnityEngine;
using UnityEngine.InputSystem;

public class JumpAttackState : State
{
    private const float _attackLength = .25f;
    private Timer _attackTimer = Timer.CreateTimer(_player, "Jump Attack Timer", OnAttackEnd, _attackLength, repeatable: false);

    public override void OnStart()
    {
        Debug.Log("attack staterd");
        _renderer.color = Color.red;
        _attackTimer.StartTimer();
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        Debug.Log("attack finished");
        _renderer.color = Color.white;
    }

    private static void OnAttackEnd()
    {
        if (MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new IdleState()));
            return;
        }
        _controller.AddStateToQueue(new StateQueueData(new FallState()));
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState(), .3f));
    }
}
