public class SuccessNode : Node
{
    private readonly Node _childNode;

    public SuccessNode(Node childNode, Weight weightComponent = null) : base(weightComponent)
    {
        _childNode = childNode;
    }

    public override NodeStates Evaluate()
    {
        _childNode.Evaluate();
        return NodeStates.Success;
    }
}