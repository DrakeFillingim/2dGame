using System.Linq;
public class WeightedSequenceNode : Node
{
    private WeightedNode[] _childNodes;
    private int _runningNode = 0;

    public WeightedSequenceNode(WeightedNode[] childNodes)
    {
        _childNodes = childNodes;
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

    private void CheckIfSort()
    {
        if (_childNodes.Any(x => x.Dirty == true))
        {
            SortChildren();
            for (int i = 0; i < _childNodes.Length; i++)
            {
                _childNodes[i].Clean();
            }
        }
    }

    private void SortChildren()
    {
        Node runningNode = _childNodes[_runningNode];
        _childNodes = _childNodes.OrderBy(x => x.NodeWeight).ToArray();
        _runningNode = System.Array.IndexOf(_childNodes, runningNode);
        UnityEngine.Debug.Log("sorted kids");
    }
}
