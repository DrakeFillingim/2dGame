using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif

public class CustomTapInteraction : IInputInteraction
{
    public float TapTime = 0.2f;

    public void Process(ref InputInteractionContext context)
    {
        if (context.timerHasExpired)
        {
            context.Canceled();
            return;
        }
        if (context.control.IsActuated())
        {
            context.Started();
            context.SetTimeout(PlayerStats.AttackTapTime);
            return;
        }
        if (context.isStarted && !context.control.IsActuated())
        {
            context.Performed();
        }
    }

    public void Reset()
    {

    }

    static CustomTapInteraction()
    {
        InputSystem.RegisterInteraction<CustomTapInteraction>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {

    }

}