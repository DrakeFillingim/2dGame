/// <summary>
/// Checks if the bool given in a delegate is equal to the given target value
/// </summary>
public class CheckBoolLeaf : Node
{
    private readonly System.Func<bool> GetValue;

    public CheckBoolLeaf(System.Func<bool> getValue, Weight weightComponent = null) : base(weightComponent)
    {
        GetValue = getValue;
    }

    public override NodeStates Evaluate()
    {
        if (GetValue())
        {
            return NodeStates.Success;
        }
        return NodeStates.Failure;
    }
}