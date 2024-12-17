/// <summary>
/// Runs each child in order, if any succeed then this node succeeds,
/// if all fail then this node fails.
/// </summary>
public class PrioritizedSelectorNode : Node
{
    private readonly Node[] _childNodes;
    private int _runningNode = 0;

    public PrioritizedSelectorNode(Node[] childNodes, float actionWeight = 0, float baseWeight = 0, float decrementTime = 0, bool lerpWeight = true) : 
        base(actionWeight, baseWeight, decrementTime, lerpWeight)
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
                    continue;
                case NodeStates.Running:
                    _runningNode = i;
                    _nodeState = NodeStates.Running;
                    return _nodeState;
                case NodeStates.Success:
                    _runningNode = 0;
                    _nodeState = NodeStates.Success;
                    return _nodeState;
            }
        }
        _nodeState = NodeStates.Failure;
        return _nodeState;
    }
}