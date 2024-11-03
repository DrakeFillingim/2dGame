using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchState : State
{
    public const float CrouchSpeed = 2.5f;

    public override void OnStart()
    {
        _stats.MovementSpeed = CrouchSpeed;
        //change this to edit sprite AND hitbox size
        _player.transform.localScale = new Vector3(_player.transform.localScale.x, _player.transform.localScale.y / 2, _player.transform.localScale.z);
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        //obviously change this too
        _player.transform.localScale = new Vector3(_player.transform.localScale.x, _player.transform.localScale.y * 2, _player.transform.localScale.z);
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new WalkState(), 0));
        _controller.AddStateToQueue(new StateQueueData(new JumpState(), Time.deltaTime + 0.1f));
    }

    protected override void OnCrouch(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new IdleState(), 0));
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new RunState(), 0));
    }
}
