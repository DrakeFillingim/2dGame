using UnityEngine;

public abstract class LeafNode : Node
{
    protected GameObject _agent;

    public LeafNode(GameObject agent, Weight weightComponent) : base(weightComponent)
    {
        _agent = agent;
    }
}