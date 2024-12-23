using UnityEngine;

public class ParryLeaf : Node
{
    private const float ParryLength = .2f;

    private float _currentParryTime = 0;

    public ParryLeaf(Weight weightComponent = null) : base(weightComponent)
    {

    }

    public override NodeStates Evaluate()
    {
        _currentParryTime += Time.deltaTime;
        if (_currentParryTime >= ParryLength)
        {
            RaiseNodeSuccess();
            Debug.Log("parrying with weight: " + WeightComponent.Value);
            _currentParryTime = 0;
            return NodeStates.Success;
        }
        Debug.Log("parrying");
        return NodeStates.Running;
    }
}
