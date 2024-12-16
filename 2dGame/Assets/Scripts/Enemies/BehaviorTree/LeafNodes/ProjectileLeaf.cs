using UnityEngine;

public class ProjectileLeaf : WeightedNode
{
    private bool run = false;
    public ProjectileLeaf()
    {
        Initialize(1, .5f);
    }

    public override NodeStates Evaluate()
    {
        Debug.Log("in projectile: " + NodeWeight);
        if (!run)
        {
            OnSuccess();
            run = true;
        }
        return NodeStates.Success;
    }
}
