using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchState : State
{
    public const float CrouchSpeed = 2.5f;
    private static Sprite _crouchSprite = Resources.Load<Sprite>("Sprites/Player/testPlayerCrouch");
    private static Sprite _normalSprite = Resources.Load<Sprite>("Sprites/Player/testPlayer");
    private static BoxCollider2D _hitbox = _player.GetComponent<BoxCollider2D>();

    public override void OnStart()
    {
        _stats.MovementSpeed = CrouchSpeed;
        _renderer.sprite = _crouchSprite;
        _hitbox.size = _renderer.sprite.bounds.size;
        _player.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y - _hitbox.size.y / 2);
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        if (!MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new FallState(), 0));
        }
    }

    public override void OnExit()
    {
        _player.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y + _hitbox.size.y / 2);
        _renderer.sprite = _normalSprite;
        _hitbox.size = _renderer.sprite.bounds.size;
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new WalkState()));
        _controller.AddStateToQueue(new StateQueueData(new JumpState(), Time.deltaTime + 0.1f));
    }

    protected override void OnDash(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new DashState()));
    }

    protected override void OnCrouch(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new IdleState()));
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new RunState()));
    }
}
