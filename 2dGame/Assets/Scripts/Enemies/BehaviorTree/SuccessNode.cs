public class SuccessNode : Node
{
    private readonly Node _childNode;

    public SuccessNode(Node childNode)
    {
        _childNode = childNode;
    }

    public override NodeStates Evaluate()
    {
        _childNode.Evaluate();
        return NodeStates.Success;
    }
}