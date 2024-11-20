public abstract class Node
{
    public enum NodeStates
    {
        Failure,
        Success,
        Running
    }

    public NodeStates nodeState;

    public abstract NodeStates Evaluate();
}