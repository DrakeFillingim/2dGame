/// <summary>
/// Runs each child in order, if any fail then this node fails,
/// if all succeed then this node succeeds.
/// </summary>
public class PrioritizedSequenceNode : Node
{
    private readonly Node[] _childNodes;
    private int _runningNode = 0;

    public PrioritizedSequenceNode(Node[] childNodes, Weight weightComponent = null) : base(weightComponent)
    {
        _childNodes = childNodes;
    }

    public override NodeStates Evaluate()
    {
        for (int i = _runningNode; i < _childNodes.Length; i++)
        {
            switch (_childNodes[i].Evaluate())
            {
                case NodeStates.Failure:
                    _runningNode = 0;
                    _nodeState = NodeStates.Failure;
                    return _nodeState;
                case NodeStates.Running:
                    _runningNode = i;
                    _nodeState = NodeStates.Running;
                    return _nodeState;
                case NodeStates.Success:
                    _runningNode = 0;
                    continue;
            }
        }
        _nodeState = NodeStates.Success;
        return _nodeState;
    }
}