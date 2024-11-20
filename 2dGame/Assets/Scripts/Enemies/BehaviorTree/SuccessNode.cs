public class SuccessNode : Node
{
    private Node childNode;

    public SuccessNode(Node childNode_)
    {
        childNode = childNode_;
    }

    public override NodeStates Evaluate()
    {
        childNode.Evaluate();
        return NodeStates.Success;
    }
}