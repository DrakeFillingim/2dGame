public class InverterNode : Node
{
    private Node childNode;

    public InverterNode(Node childNode_)
    {
        childNode = childNode_;
    }

    public override NodeStates Evaluate()
    {
        switch (childNode.Evaluate())
        {
            case NodeStates.Failure:
                nodeState = NodeStates.Success;
                break;
            case NodeStates.Success:
                nodeState = NodeStates.Failure;
                break;
            case NodeStates.Running:
                nodeState = NodeStates.Running;
                break;
        }
        return nodeState;
    }
}