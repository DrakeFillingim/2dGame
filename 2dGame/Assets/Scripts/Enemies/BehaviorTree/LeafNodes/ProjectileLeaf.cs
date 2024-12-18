using UnityEngine;

public class ProjectileLeaf : Node
{
    private bool run = false;
    public ProjectileLeaf()
    {

    }

    public override NodeStates Evaluate()
    {
        Debug.Log("in projectile: " + WeightComponent.Value);
        if (!run)
        {
            WeightComponent.OnSuccess();
            run = true;
        }
        return NodeStates.Success;
    }
}
