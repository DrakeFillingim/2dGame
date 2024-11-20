public class LeafNode : Node
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
                nodeState = NodeStates.Success;
                break;
            case NodeStates.Running:
                nodeState = NodeStates.Running;
                break;
            case NodeStates.Failure:
                nodeState = NodeStates.Failure;
                break;
        }
        return nodeState;
    }
}