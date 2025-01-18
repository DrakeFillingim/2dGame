using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttackLeaf : LeafNode
{
    private float _attackTime = 0;
    private float _lockTime = .175f;
    private float _currentAttackTime = 0;

    private int _animationHash;

    private System.Func<bool> _getPlayerAttacking;

    public LightAttackLeaf(string animationName, System.Func<bool> getPlayerAttacking, GameObject agent, Weight weightComponent = null) : base(agent, weightComponent)
    {
        _attackTime = _animator.GetClip(animationName).length;
        _animationHash = Animator.StringToHash(animationName);
        _getPlayerAttacking = getPlayerAttacking;
    }

    public override NodeStates Evaluate()
    {
        _animator.Play(_animationHash);
        _currentAttackTime += Time.deltaTime;
        if (_getPlayerAttacking() && _currentAttackTime <= _lockTime)
        {
            return NodeStates.Failure;
        }
        if (_currentAttackTime >= _attackTime)
        {
            _currentAttackTime = 0;
            RaiseNodeSuccess();
            return NodeStates.Success;
        }
        return NodeStates.Running;
    }
}
