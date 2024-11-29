/*public class LeafNode : Node
{
    public System.Func<NodeStates> nodeAction;

    public LeafNode(System.Func<NodeStates> nodeAction_)
    {
        nodeAction = nodeAction_;
    }

    public override NodeStates Evaluate()
    {
        switch (nodeAction())
        {
            case NodeStates.Success:
                _nodeState = NodeStates.Success;
                break;
            case NodeStates.Running:
                _nodeState = NodeStates.Running;
                break;
            case NodeStates.Failure:
                _nodeState = NodeStates.Failure;
                break;
        }
        return _nodeState;
    }
}*/