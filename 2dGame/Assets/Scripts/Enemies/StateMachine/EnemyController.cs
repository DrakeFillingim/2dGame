using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyState currentState;

    private Transform _player;

    private void Start()
    {
        _player = GameObject.Find("Player").transform;

        EnemyState idleState = new EnemyIdle(gameObject, "Idle");
        EnemyState attackState = new EnemyAttack(gameObject, "Attack");
        EnemyState parryState = new EnemyParry(gameObject, "Parry");
        EnemyState waitState = new EnemyWait(gameObject, "Idle");
        EnemyState parriedState = new EnemyParried(gameObject, "Parried");

        idleState.ConnectEvents(onPlayerEntersRange: () => SetState(attackState));
        attackState.ConnectEvents(() => SetState(parryState), onStateFinish: () => SetState(waitState), onPlayerParry: () => SetState(parriedState));
        waitState.ConnectEvents(onStateFinish: () => SetState(idleState));
        parriedState.ConnectEvents(onStateFinish: () => SetState(idleState));

        currentState = idleState;
        idleState.OnEnter();
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    /*private void FixedUpdate()
    {
        UpdateRotation();
    }*/

    private void SetState(EnemyState newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }

    private void UpdateRotation()
    {
        if (_player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
