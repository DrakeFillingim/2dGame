public class InverterNode : Node
{
    private readonly Node _childNode;

    public InverterNode(Node childNode)
    {
        _childNode = childNode;
    }

    public override NodeStates Evaluate()
    {
        switch (_childNode.Evaluate())
        {
            case NodeStates.Failure:
                _nodeState = NodeStates.Success;
                break;
            case NodeStates.Success:
                _nodeState = NodeStates.Failure;
                break;
            case NodeStates.Running:
                _nodeState = NodeStates.Running;
                break;
        }
        return _nodeState;
    }
}