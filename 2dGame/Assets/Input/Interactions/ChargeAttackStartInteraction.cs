using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif

public class ChargeAttackStartInteraction : IInputInteraction
{
    private enum States
    {
        WaitingForInput,
        WaitingForTapEnd,
        WaitingForChargeEnd,
    }
    private States _currentState = States.WaitingForInput;

    public void Process(ref InputInteractionContext context)
    {
        if (context.control.IsActuated())
        {
            if (_currentState == States.WaitingForInput)
            {
                context.SetTimeout(PlayerStats.AttackTapTime);
                _currentState = States.WaitingForTapEnd;
            }
            else if (_currentState == States.WaitingForTapEnd && context.timerHasExpired)
            {
                context.Started();
                context.SetTimeout(PlayerStats.AttackChargeTime);
                _currentState = States.WaitingForChargeEnd;
            }
            else if (_currentState == States.WaitingForChargeEnd && context.timerHasExpired)
            {
                context.Performed();
                _currentState = States.WaitingForInput;
            }
        }
        else
        {
            if (_currentState == States.WaitingForChargeEnd)
            {
                context.Canceled();

            }
            _currentState = States.WaitingForInput;
        }
    }

    public void Reset()
    {
        _currentState = States.WaitingForInput;
    }

    static ChargeAttackStartInteraction()
    {
        InputSystem.RegisterInteraction<ChargeAttackStartInteraction>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {

    }

}
