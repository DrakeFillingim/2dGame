/// <summary>
/// Base class for all nodes to inherit from, must override <c>Evaluate</c>
/// </summary>
public abstract class Node
{
    public enum NodeStates
    {
        Failure,
        Success,
        Running
    }
    protected NodeStates _nodeState;

    public abstract NodeStates Evaluate();
}