/// <summary>
/// Checks if the bool given in a delegate is equal to the given target value
/// </summary>
public class CheckBoolLeaf : Node
{
    private readonly System.Func<bool> GetValue;
    private readonly bool _targetValue;

    public CheckBoolLeaf(System.Func<bool> getValue, bool targetValue, Weight weightComponent = null) : base(weightComponent)
    {
        GetValue = getValue;
        _targetValue = targetValue;
    }

    public override NodeStates Evaluate()
    {
        if (_targetValue == GetValue())
        {
            return NodeStates.Success;
        }
        return NodeStates.Failure;
    }
}