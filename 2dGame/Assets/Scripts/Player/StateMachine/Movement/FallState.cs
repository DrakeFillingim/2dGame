using UnityEngine;
using UnityEngine.InputSystem;

public class FallState : State
{
    public override void OnStart()
    {

    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        
        if (MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new IdleState(), destructable: true));
        }
    }

    public override void OnExit()
    {
        
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState()));
    }

    protected override void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("fall dash");
        _controller.AddStateToQueue(new StateQueueData(new DashState()));
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpAttackState()));
    }
}
