/// <summary>
/// Runs each child in order, if any succeed then this node succeeds,
/// if all fail then this node fails. Will re-sort each child when any of
/// their weights change.
/// </summary>
public class WeightedSelectorNode : WeightedCompositeNode
{
    public WeightedSelectorNode(Node[] childNodes, float actionWeight = 0, float baseWeight = 0, float decrementTime = 0, bool lerpWeight = true) :
        base(childNodes, actionWeight, baseWeight, decrementTime, lerpWeight)
    {

    }

    public override NodeStates Evaluate()
    {
        CheckIfSort();
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