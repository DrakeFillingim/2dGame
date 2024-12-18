using UnityEngine;

public class ParryLeaf : Node
{

    public ParryLeaf()
    {

    }

    public override NodeStates Evaluate()
    {
        Debug.Log("in parry: " + WeightComponent.Value);
        WeightComponent.OnSuccess();
        return NodeStates.Success;
    }
}
