using UnityEngine;

public class EnemyParry : EnemyState
{
    private Animator _animator;
    private int _animationHash;

    public EnemyParry(GameObject agent, string animationName)
    {
        _animator = agent.GetComponent<Animator>();
        _animationHash = Animator.StringToHash(animationName);
    }

    public override void OnEnter()
    {
        _animator.Play(_animationHash);
    }

    public override void OnUpdate()
    {
        Debug.Log("parrying");
    }
}
