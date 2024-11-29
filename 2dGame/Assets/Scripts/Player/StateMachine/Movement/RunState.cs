using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunState : State
{
    public const float RunSpeed = 15;
    private const float SlideWindow = 0.2f;

    private Timer _canSlide;

    private bool _lerpSpeed = false;

    public override void OnStart()
    {
        if (_canSlide == null)
        {
            _canSlide = Timer.CreateTimer(_player, "CanSlideTimer", () => OnSlideTimeout(), SlideWindow, repeatable: false);
        }
        _stats.MovementSpeed = RunSpeed;
        if (!_inputMap["Run"].IsPressed())
        {
            _controller.AddStateToQueue(new StateQueueData(new WalkState()));
        }
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {
        if (_rb.velocity == Vector2.zero && !_inputMap["Move"].IsPressed())
        {
            _controller.AddStateToQueue(new StateQueueData(new IdleState()));
        }
        if (!MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new FallState(), destructable: true));
            _lerpSpeed = true;
        }
    }

    public override void OnExit()
    {
        if (_canSlide != null)
        {
            Object.Destroy(_canSlide);
        }
        if (_lerpSpeed)
        {
            CoroutineRunner.CreateCoroutine(_player, LerpRunAirSpeed(_stats.MovementSpeed, WalkState.WalkSpeed));
        }
    }

    private void OnSlideTimeout()
    {
        _stats.MovementSpeed = WalkState.WalkSpeed;
        if (MovementHelper.IsGrounded(_player, _stats.GravityDirection))
        {
            _controller.AddStateToQueue(new StateQueueData(new WalkState()));
        }
        else
        {
            _controller.AddStateToQueue(new StateQueueData(new FallState()));
            _lerpSpeed = true;
        }
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState()));
        _lerpSpeed = true;
    }

    protected override void OnDash(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new DashState()));
    }

    protected override void OnCrouch(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new SlideState()));
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        _canSlide.StartTimer();
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new MovementAttackState()));
    }

    protected override void OnChargeAttackStarted(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new ChargeAttackState()));
    }

    private IEnumerator LerpRunAirSpeed(float startSpeed, float targetSpeed)
    {
        float maxTime = 1f;
        float currentTime = 0;
        float previousSet = startSpeed;

        while (currentTime <= maxTime)
        {
            // Ends the coroutine if speed has been set from somewhere else
            if (_stats.MovementSpeed != previousSet)
            {
                Debug.Log("broken");
                yield break;
            }
            _stats.MovementSpeed = Mathf.Lerp(startSpeed, targetSpeed, currentTime / maxTime);
            previousSet = _stats.MovementSpeed;
            currentTime += Time.deltaTime;
            yield return null;
        }
        _stats.MovementSpeed = targetSpeed;
        yield break;
    }
}
