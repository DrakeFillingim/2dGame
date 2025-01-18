using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleLeaf : LeafNode
{
    private int _animationHash;

    public IdleLeaf(string animationName, GameObject agent, Weight weightComponent = null) : base(agent, weightComponent)
    {
        _animationHash = Animator.StringToHash(animationName);
    }

    public override NodeStates Evaluate()
    {
        Debug.Log("set idle");
        _animator.Play(_animationHash);
        return NodeStates.Success;
    }
}
