using UnityEngine;

public class ParryLeaf : Node
{

    public ParryLeaf() : base(0.4f)
    {

    }

    public override NodeStates Evaluate()
    {
        Debug.Log("in parry: " + NodeWeight);
        //OnSuccess();
        return NodeStates.Success;
    }
}
