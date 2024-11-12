using UnityEngine;
using UnityEngine.InputSystem;

public class LightAttackState : State
{
    public override void OnStart()
    {
        Debug.Log("Started light attack");
        _controller.AddStateToQueue(new StateQueueData(new IdleState()));
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }
}