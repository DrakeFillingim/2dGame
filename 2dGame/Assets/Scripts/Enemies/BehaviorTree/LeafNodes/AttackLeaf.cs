using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLeaf : Node
{
    public AttackLeaf()
    {

    }

    public override NodeStates Evaluate()
    {
        return NodeStates.Failure;
    }
}
