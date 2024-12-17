using UnityEngine;

public class ProjectileLeaf : Node
{
    private bool run = false;
    public ProjectileLeaf() : base(1, 0.5f)
    {

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
