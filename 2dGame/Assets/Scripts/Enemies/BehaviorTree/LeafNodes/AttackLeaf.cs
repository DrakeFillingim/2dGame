using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLeaf : WeightedNode
{
    public AttackLeaf()
    {
        Initialize(0);
    }

    public override NodeStates Evaluate()
    {
        return NodeStates.Failure;
    }
}
