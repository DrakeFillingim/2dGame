using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif

public class ChargeAttackInteraction : IInputInteraction
{
    public float TapTime = 0.2f;

    private enum States
    {
        WaitingForInput,
        WaitingForTapEnd,
        HeldDown,
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
                _currentState = States.HeldDown;
                context.Started();
            }
        }
        else
        {
            if (_currentState == States.HeldDown)
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

    static ChargeAttackInteraction()
    {
        InputSystem.RegisterInteraction<ChargeAttackInteraction>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {

    }

}