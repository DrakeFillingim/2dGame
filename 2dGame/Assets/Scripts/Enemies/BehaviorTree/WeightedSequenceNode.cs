/// <summary>
/// Runs each child in order, if any fail then this node fails,
/// if all succeed then this node succeeds. Will re-sort each child when
/// any of their weights change.
/// </summary>
public class WeightedSequenceNode : WeightedCompositeNode
{
    public WeightedSequenceNode(Node[] childNodes, Weight weightComponent = null) : base(childNodes, weightComponent)
    {

    }

    public override NodeStates Evaluate()
    {
        CheckIfSort();
        UnityEngine.Debug.Log(_runningNode);
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
