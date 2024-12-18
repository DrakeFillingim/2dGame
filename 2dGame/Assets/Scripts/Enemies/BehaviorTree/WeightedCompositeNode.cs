using System.Linq;

/// <summary>
/// Provides methods to check if the child nodes need to be sorted and sort them.
/// </summary>
public abstract class WeightedCompositeNode : Node
{
    protected Node[] _childNodes;
    protected int _runningNode;

    public WeightedCompositeNode(Node[] childNodes)
    {
        _childNodes = childNodes;
    }

    protected void CheckIfSort()
    {
        if (_childNodes.Any(x => x.WeightComponent.Dirty == true))
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
        Node runningNode = _childNodes[_runningNode];
        _childNodes = _childNodes.OrderBy(x => x.WeightComponent.Value).ToArray();
        _runningNode = System.Array.IndexOf(_childNodes, runningNode);
        UnityEngine.Debug.Log("sorted kids");
    }
}