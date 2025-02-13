using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWait : EnemyState
{
    private Animator _animator;
    private int _animationHash;


    private const float WaitTime = 1;
    private float _currentWaitTime = 0;

    public EnemyWait(GameObject agent, string animationName)
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
        _currentWaitTime += Time.deltaTime;
        if (_currentWaitTime > WaitTime)
        {
            _onStateFinish?.Invoke();
        }
    }
}
