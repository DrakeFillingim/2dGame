using UnityEngine;

public class ParryLeaf : LeafNode
{
    //two different nodes? one for transition, fails on getting hit
    //two major parts: transition to parry (very short, play parry anim), then parry for x time
    //return success for blocking an attack, fail for missing -> counterattack or idle

    private float parryLength = 0;

    private float _currentParryTime = 0;

    private int _animationHash;

    public ParryLeaf(string animationName, GameObject agent, Weight weightComponent = null) : base(agent, weightComponent)
    {
        parryLength = _animator.GetClip(animationName).length;
        _animationHash = Animator.StringToHash(animationName);
    }

    public override NodeStates Evaluate()
    {
        _animator.Play(_animationHash);
        _currentParryTime += Time.deltaTime;
        if (_currentParryTime >= parryLength)
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
