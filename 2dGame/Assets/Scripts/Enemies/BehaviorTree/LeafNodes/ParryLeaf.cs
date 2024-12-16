using UnityEngine;

public class ParryLeaf : WeightedNode
{

    public ParryLeaf()
    {
        Initialize(.4f);
    }

    public override NodeStates Evaluate()
    {
        Debug.Log("in parry: " + NodeWeight);
        //OnSuccess();
        return NodeStates.Success;
    }
}
