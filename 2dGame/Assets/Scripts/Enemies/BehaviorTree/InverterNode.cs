/// <summary>
/// Decorator node that returns the opposite of what its child returns.
/// Failure -> Success, Success -> Failure, Running -> Running.
/// </summary>
public class InverterNode : Node
{
    private readonly Node _childNode;

    public InverterNode(Node childNode, float actionWeight = 0, float baseWeight = 0, float decrementTime = 1, bool lerpWeight = true) : 
        base(actionWeight, baseWeight, decrementTime, lerpWeight)
    {
        _childNode = childNode;
    }

    public override NodeStates Evaluate()
    {
        switch (_childNode.Evaluate())
        {
            case NodeStates.Failure:
                _nodeState = NodeStates.Success;
                break;
            case NodeStates.Success:
                _nodeState = NodeStates.Failure;
                break;
            case NodeStates.Running:
                _nodeState = NodeStates.Running;
                break;
        }
        return _nodeState;
    }
}