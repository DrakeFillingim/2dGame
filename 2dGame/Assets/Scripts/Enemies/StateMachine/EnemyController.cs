using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyState currentState;

    private void Start()
    {
        EnemyState idleState = new EnemyIdle(gameObject, "Idle");
        EnemyState attackState = new EnemyAttack(gameObject, "Attack 1");
        EnemyState parryState = new EnemyParry(gameObject, "Parry");

        idleState.ConnectEvents(onPlayerEntersRange: () => SetState(attackState));
        attackState.ConnectEvents(() => SetState(parryState));

        currentState = idleState;
        idleState.OnEnter();
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    private void SetState(EnemyState newState)
    {
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter();
    }
}
