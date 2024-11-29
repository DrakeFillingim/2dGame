using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLeaf : WeightedNode
{
    public ProjectileLeaf()
    {
        Initialize(.5f, 1, 1, false);
    }

    public override NodeStates Evaluate()
    {
        Debug.Log("in projectile: " + NodeWeight);
        return NodeStates.Success;
    }
}
