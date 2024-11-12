using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif

public class ChargeTimedHoldInteraction : IInputInteraction
{
    public float TapTime;
    public float HoldTime;

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
                _currentState = States.WaitingForTapEnd;
                context.SetTimeout(TapTime);
            }
            else if (_currentState == States.WaitingForTapEnd && context.timerHasExpired)
            {
                _currentState = States.WaitingForChargeEnd;
                context.Started();
                context.SetTimeout(HoldTime);
            }
            else if (_currentState == States.WaitingForChargeEnd && context.timerHasExpired)
            {
                _currentState = States.WaitingForInput;
                context.Performed();
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

    static ChargeTimedHoldInteraction()
    {
        InputSystem.RegisterInteraction<ChargeTimedHoldInteraction>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {

    }

}