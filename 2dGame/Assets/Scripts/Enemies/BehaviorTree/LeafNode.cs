using UnityEngine;

public abstract class LeafNode : Node
{
    protected GameObject _agent;

    protected Animator _animator;

    public LeafNode(GameObject agent, Weight weightComponent) : base(weightComponent)
    {
        _agent = agent;
        _animator = _agent.GetComponent<Animator>();
    }
}