using UnityEngine;
using UnityEngine.InputSystem;

public class ParryState : State
{
    private static float? parryTime = .25f;
    private float _currentParryTime = 0;

    public override void OnStart()
    {
        _stats.CombatState = DamageHandler.CombatStates.Parrying;

        _animator.Play("Parry");
        if (parryTime == null)
        {
            _animator.Update(0);
            //parryTime = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }
        _stats.MovementSpeed = 0;
        _inputMap["Parry"].canceled += OnParryCanceled;
    }

    public override void OnUpdate()
    {
        _currentParryTime += Time.deltaTime;
        if (_currentParryTime >= parryTime)
        {
            _controller.AddStateToQueue(new StateQueueData(new BlockState()));
        }
    }

    public override void OnFixedUpdate()
    {
        
    }

    public override void OnExit()
    {
        _stats.CombatState = DamageHandler.CombatStates.Undefended;
        _inputMap["Parry"].canceled -= OnParryCanceled;
    }

    private void OnParryCanceled(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new IdleState()));
    }
}
