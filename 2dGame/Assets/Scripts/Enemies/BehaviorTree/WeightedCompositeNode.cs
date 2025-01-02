using System.Linq;

/// <summary>
/// Provides methods to check if the child nodes need to be sorted and sort them.
/// </summary>
public abstract class WeightedCompositeNode : Node
{
    protected Node[] _childNodes;
    protected int _runningNode;

    public WeightedCompositeNode(Node[] childNodes, Weight weightComponent) : base (weightComponent)
    {
        _childNodes = childNodes;
    }

    protected void CheckIfSort()
    {
        if (_childNodes.Any(x => x.WeightComponent.Dirty == true) && _nodeState != NodeStates.Running)
        {
            SortChildren();
            for (int i = 0; i < _childNodes.Length; i++)
            {
                _childNodes[i].WeightComponent.Dirty = false;
            }
        }
    }

    private void SortChildren()
    {
        _childNodes = _childNodes.OrderBy(x => x.WeightComponent.Value).ToArray();
        UnityEngine.Debug.Log("sorted kids");
        foreach (Node n in _childNodes)
        {
            UnityEngine.Debug.Log(n.GetType() + " " + n.WeightComponent.Value);
        }
    }
}