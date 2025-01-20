using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    private Animator _animator;
    private int _animationHash;

    public EnemyIdle(GameObject agent, string animationName)
    {
        _animator = agent.GetComponent<Animator>();
        _animationHash = Animator.StringToHash(animationName);

        agent.GetComponent<SensorLayer>().PlayerEntersRange += OnPlayerEntersRange;
    }

    public override void OnEnter()
    {
        _animator.Play(_animationHash);
    }

    public override void OnUpdate()
    {
        Debug.Log("in idle");
    }

    protected override void OnPlayerEntersRange()
    {
        _onPlayerEntersRange?.Invoke();
    }
}
