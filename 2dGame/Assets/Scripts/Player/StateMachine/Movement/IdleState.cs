using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : State
{
    public override void OnStart()
    {

    }

    public override void OnUpdate()
    {
        Debug.Log("in idle");
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnExit()
    {

    }

    protected override void OnMove(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() != 0)
        {
            _controller.AddStateToQueue(new StateQueueData(new WalkState(), 0));
        }
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState(), 0));
    }
}
