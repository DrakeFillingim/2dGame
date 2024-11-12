using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif

public class ChargeHoldInteraction : IInputInteraction
{
    public float TapTime;

    private enum States
    {
        WaitingForInput,
        WaitingForTapEnd,
        Activated
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
                _currentState = States.Activated;
                context.Started();
            }
        }
        else
        {
            if (_currentState == States.Activated)
            {
                _currentState = States.WaitingForInput;
                context.Canceled();
            }
            else
            {
                _currentState = States.WaitingForInput;
            }
        }
    }

    public void Reset()
    {
        _currentState = States.WaitingForInput;
    }

    static ChargeHoldInteraction()
    {
        InputSystem.RegisterInteraction<ChargeHoldInteraction>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {

    }
}