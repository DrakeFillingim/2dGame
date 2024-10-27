using UnityEngine;
using UnityEngine.InputSystem;

public class IdleState : State
{
    public override void OnStart()
    {
        _inputMap["Move"].performed += OnMove;
        Debug.Log("controller: " + _controller);
    }

    public override void OnUpdate()
    {
        Debug.Log("In Idle");
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
        if (context.ReadValue<float>() != 0)
        {
            _controller.AddStateToQueue(new StateQueueData(new WalkState(), 0));
        }
    }
}
