using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpState : State
{
    private const float JumpHeight = 6;
    private const float JumpGravity = 30;
    private static float JumpForce = Mathf.Sqrt(2 * JumpGravity * JumpHeight);

    private float _startingGravity;

    public override void OnStart()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, 0);
        _rb.AddForce(-_stats.GravityDirection * JumpForce, ForceMode2D.Impulse);
        _startingGravity = _stats.GravityScale;
        _stats.GravityScale = JumpGravity;

        if (_controller.previousStates.Pop().GetType() == typeof(SlideState))
        {
            Debug.Log("slide to jump");
            CoroutineRunner.CreateCoroutine(_player, LerpAirSlideSpeed());
        }
    }

    public override void OnUpdate()
    {

    }

    //set fall after jump anim completed?
    public override void OnFixedUpdate()
    {
        if (_rb.velocity.y <= 0)
        {
            _controller.AddStateToQueue(new StateQueueData(new FallState(), destructable: true));
        }
    }

    public override void OnExit()
    {
        _stats.GravityScale = _startingGravity;
    }

    protected override void OnJump(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpState()));
    }

    protected override void OnDash(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new DashState()));
    }

    protected override void OnRunStarted(InputAction.CallbackContext context)
    {
        _stats.MovementSpeed = RunState.RunSpeed;
    }

    protected override void OnRunCanceled(InputAction.CallbackContext context)
    {
        _stats.MovementSpeed = WalkState.WalkSpeed;
    }

    protected override void OnAttack(InputAction.CallbackContext context)
    {
        _controller.AddStateToQueue(new StateQueueData(new JumpAttackState(), .75f));
    }

    private IEnumerator LerpAirSlideSpeed()
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
            _stats.MovementSpeed = Mathf.Lerp(RunState.RunSpeed, WalkState.WalkSpeed, currentTime / maxTime);
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}
