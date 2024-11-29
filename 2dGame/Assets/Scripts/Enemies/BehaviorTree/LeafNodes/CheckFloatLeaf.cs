/// <summary>
/// Checks if the float given in a delegate is greater than or equal to the given target value
/// </summary>
public class CheckFloatLeaf : Node
{
    private readonly System.Func<float> GetValue;
    private readonly float _targetValue;

    public CheckFloatLeaf(System.Func<float> getValue, float targetValue)
    {
        GetValue = getValue;
        _targetValue = targetValue;
    }

    public override NodeStates Evaluate()
    {
        if (_targetValue >= GetValue())
        {
            return NodeStates.Success;
        }
        return NodeStates.Failure;
    }
}