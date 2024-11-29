using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryLeaf : WeightedNode
{

    public ParryLeaf()
    {
        Initialize(0, .4f, 1, true);
    }

    public override NodeStates Evaluate()
    {
        Debug.Log("in parry: " + NodeWeight);
        OnSuccess();
        return NodeStates.Success;
    }
}
