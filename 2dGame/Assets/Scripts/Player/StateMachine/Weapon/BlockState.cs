using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State
{
    private const float BlockSpeed = 1.5f;

    public override void OnStart()
    {
        _stats.MovementSpeed = BlockSpeed;
        _animator.SetInteger("State", (int)StateTypes.Block);
        _animator.Play("Block");
        _inputMap["Parry"].canceled += x => _controller.AddStateToQueue(new StateQueueData(new IdleState()));
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
