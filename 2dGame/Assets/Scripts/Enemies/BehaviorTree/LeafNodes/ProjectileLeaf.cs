using UnityEngine;

public class ProjectileLeaf : Node
{
    private bool run = false;
    public ProjectileLeaf(Weight weightComponent) : base(weightComponent)
    {

    }

    public override NodeStates Evaluate()
    {
        Debug.Log("in projectile: " + WeightComponent.Value);
        return NodeStates.Success;
    }
}
