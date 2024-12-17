using System.Linq;

/// <summary>
/// Provides methods to check if the child nodes need to be sorted and sort them.
/// </summary>
public abstract class WeightedCompositeNode : Node
{
    protected Node[] _childNodes;
    protected int _runningNode;

    public WeightedCompositeNode(Node[] childNodes, float actionWeight = 0, float baseWeight = 0, float decrementTime = 0, bool lerpWeight = true) :
        base(actionWeight, baseWeight, decrementTime, lerpWeight)
    {
        _childNodes = childNodes;
    }

    protected void CheckIfSort()
    {
        if (_childNodes.Any(x => x.Dirty == true))
        {
            SortChildren();
            for (int i = 0; i < _childNodes.Length; i++)
            {
                _childNodes[i].Dirty = false;
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