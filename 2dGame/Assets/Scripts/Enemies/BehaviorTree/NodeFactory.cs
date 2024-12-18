using System;

public static class NodeFactory
{
    public static Node CreateCompositeNode<T>(Node[] childNodes, float actionWeight = 0, float baseWeight = 0, float decrementTime = 1, bool lerpWeight = true)
    {
        return CreateNode<T>(new object[] { childNodes }, actionWeight, baseWeight, decrementTime, lerpWeight);
    }

    public static Node CreateDecoratorNode<T>(Node childNode, float actionWeight = 0, float baseWeight = 0, float decrementTime = 1, bool lerpWeight = true)
    {
        return CreateNode<T>(new object[] { childNode }, actionWeight, baseWeight, decrementTime, lerpWeight);
    }

    public static Node CreateNode<T>(object[] args = null, float actionWeight = 0, float baseWeight = 0, float decrementTime = 1, bool lerpWeight = true)
    {
        Node createdNode = Activator.CreateInstance(typeof(T), args) as Node;
        createdNode.WeightComponent = new Node.Weight(actionWeight, baseWeight, decrementTime, lerpWeight);
        UnityEngine.Debug.Log(createdNode);
        return createdNode;
    }
}
