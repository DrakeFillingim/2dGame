public class SelectorNode : Node
{
    private Node[] childNodes;
    private int _runningNode = 0;

    public SelectorNode(Node[] childNodes_)
    {
        childNodes = childNodes_;
    }

    public override NodeStates Evaluate()
    {
        for (int i = _runningNode; i < childNodes.Length; i++)
        {
            switch (childNodes[i].Evaluate())
            {
                case NodeStates.Failure:
                    _runningNode = 0;
                    continue;
                case NodeStates.Running:
                    _runningNode = i;
                    nodeState = NodeStates.Running;
                    return nodeState;
                case NodeStates.Success:
                    _runningNode = 0;
                    nodeState = NodeStates.Success;
                    return nodeState;
            }
        }
        nodeState = NodeStates.Failure;
        return nodeState;
    }
}