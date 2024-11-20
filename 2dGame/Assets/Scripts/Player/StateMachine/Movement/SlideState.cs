using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlideState : State
{
    private const float MaxSlideTime = 1;
    private float _currentSlideTime = 0;
    private bool _lerpSpeed = false;

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
            if (MovementHelper.IsGrounded(_player, _stats.GravityDirection))
            {
                _controller.AddStateToQueue(new StateQueueData(new CrouchState()));
            }
            else
            {
                _controller.AddStateToQueue(new StateQueueData(new FallState(), destructable: true));
                _lerpSpeed = true;
            }
        }
    }

    public override void OnExit()
    {
        _inputMap["Move"].Enable();
        _movement.CheckMoveInput();
        if (_lerpSpeed)
        {
            CoroutineRunner.CreateCoroutine(_player, LerpSlideAirSpeed(_stats.MovementSpeed, WalkState.WalkSpeed));
        }

        _player.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y + _hitbox.size.y / 2);
        _renderer.sprite = _normalSprite;
        _hitbox.size = _renderer.sprite.bounds.size;
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState()));
        _lerpSpeed = true;
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new RunState()));
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new MovementAttackState(), .25f));
    }

    protected override void OnChargeAttackStarted(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new ChargeAttackState(), 1));
    }

    private IEnumerator LerpSlideAirSpeed(float startSpeed, float targetSpeed)
    {
        float maxTime = 0.5f;
        float currentTime = 0;

        while (currentTime <= maxTime)
        {
            if (!_inputMap["Move"].IsPressed())
            {
                _stats.MovementSpeed = WalkState.WalkSpeed;
                Debug.Log("slide coroutine canceled");
                yield break;
            }
            _stats.MovementSpeed = Mathf.Lerp(startSpeed, targetSpeed, currentTime / maxTime);
            Debug.Log(_stats.MovementSpeed);
            currentTime += Time.deltaTime;
            yield return null;
        }
        _stats.MovementSpeed = targetSpeed;
        yield break;
    }
}
