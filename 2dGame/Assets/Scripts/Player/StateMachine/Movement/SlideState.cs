using UnityEngine;
using UnityEngine.InputSystem;

public class SlideState : State
{
    private const float MaxSlideTime = .6f;
    private float _currentSlideTime = 0;

    private static Sprite _slideSprite = Resources.Load<Sprite>("Sprites/Player/testPlayerCrouch");
    private static Sprite _normalSprite = Resources.Load<Sprite>("Sprites/Player/testPlayer");
    private static BoxCollider2D _hitbox = _player.GetComponent<BoxCollider2D>();
    private static PlayerMovement _movement = _player.GetComponent<PlayerMovement>();

    public override void OnStart()
    {
        _inputMap["Move"].Disable();

        _renderer.sprite = _slideSprite;
        _hitbox.size = _renderer.sprite.bounds.size;
        _player.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y - _hitbox.size.y / 2);
    }

    public override void OnUpdate()
    {
        
    }

    public override void OnFixedUpdate()
    {
        _currentSlideTime += Time.fixedDeltaTime;
        _stats.MovementSpeed = Mathf.Lerp(RunState.RunSpeed, CrouchState.CrouchSpeed, Easings.EaseOutQuad(_currentSlideTime, MaxSlideTime));
        if (_currentSlideTime >= MaxSlideTime)
        { 
            _controller.AddStateToQueue(new StateQueueData(new CrouchState()));
        }
    }

    public override void OnExit()
    {
        _inputMap["Move"].Enable();
        _movement.CheckMoveInput();

        _player.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y + _hitbox.size.y / 2);
        _renderer.sprite = _normalSprite;
        _hitbox.size = _renderer.sprite.bounds.size;
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _stats.MovementSpeed = RunState.RunSpeed;
        _controller.AddStateToQueue(new StateQueueData(new JumpState()));
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new RunState()));
    }
}
