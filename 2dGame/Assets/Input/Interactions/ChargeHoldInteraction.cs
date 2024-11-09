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
                context.SetTimeout(TapTime);
                _currentState = States.WaitingForTapEnd;
            }

            else if (_currentState == States.WaitingForTapEnd && context.timerHasExpired)
            {
                context.Started();
                _currentState = States.Activated;
            }
        }
        else
        {
            if (_currentState == States.Activated)
            {
                context.Canceled();
                _currentState = States.WaitingForInput;
            }
            else
            {
                _currentState = States.WaitingForInput;
            }
        }
    }

    public void Reset()
    {

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