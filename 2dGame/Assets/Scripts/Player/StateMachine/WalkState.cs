using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : State
{
    public override void OnStart()
    {
        _inputMap["Move"].performed += OnMove;
    }

    public override void OnUpdate()
    {
        Debug.Log("In Walk");
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        _inputMap["Move"].performed -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() == 0)
        {
            _controller.AddStateToQueue(new StateQueueData(new IdleState(), 0));
        }
    }
}
