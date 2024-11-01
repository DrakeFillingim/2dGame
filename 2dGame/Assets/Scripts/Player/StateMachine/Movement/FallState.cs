using System.Collections;
using System.Collections.Generic;
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
            _controller.AddStateToQueue(new StateQueueData(new IdleState(), 0));
        }
    }

    public override void OnExit()
    {

    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState(), 0));
    }
}
