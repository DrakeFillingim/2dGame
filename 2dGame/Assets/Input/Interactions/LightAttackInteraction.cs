using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
[InitializeOnLoad]
#endif

public class LightAttackInteraction : IInputInteraction
{
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

    static LightAttackInteraction()
    {
        InputSystem.RegisterInteraction<LightAttackInteraction>();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {

    }

}
