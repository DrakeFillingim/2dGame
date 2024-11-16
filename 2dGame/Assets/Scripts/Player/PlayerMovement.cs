using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the basic left and right motion of the player,
/// as well as gravity and friction.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    private InputActionMap _inputMap;
    private Rigidbody2D _rb;
    private PlayerStats _stats;
    private SpriteRenderer _renderer;

    private float _inputDirection = 0;
    private bool _isGrounded = false;

    private void Start()
    {
        _inputMap = GameObject.Find("InputHandler").GetComponent<PlayerInput>().actions.FindActionMap("Player");
        _inputMap["Move"].performed += OnMove;

        _rb = GetComponent<Rigidbody2D>();
        _stats = GetComponent<PlayerStats>();
        _renderer = GetComponent<SpriteRenderer>();

        _rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        SetPlayerAcceleration();
        ResetVelocity();
        MovePlayer();
        ApplyGravity();
    }

    /// <summary>
    /// Disconnects input events to prevent duplicate events and events from destroyed input handlers
    /// </summary>
    private void OnDisable()
    {
        _inputMap["Move"].performed -= OnMove;
    }

    /// <summary>
    /// Changes the players direction upon recieving an input event
    /// </summary>
    /// <param name="context"></param>
    private void OnMove(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<float>();
        if (_inputDirection == 1)
        {
            _renderer.flipX = false; 
        }
        else if (_inputDirection == -1)
        {
            _renderer.flipX = true;
        }
    }

    /// <summary>
    /// Forces an update of the Input System and resyncs the players direction to it.
    /// </summary>
    public void CheckMoveInput()
    {
        InputSystem.Update();
        _inputDirection = _inputMap["Move"].ReadValue<float>();
    }

    /// <summary>
    /// Sets the players acceleration to 1 if it hit the ground this frame to prevent reacceleration after speed loss,
    /// and the appropriate acceleration and deceleration values otherwise.
    /// </summary>
    private void SetPlayerAcceleration()
    {
        bool groundedThisFrame = MovementHelper.IsGrounded(gameObject, _stats.GravityDirection);
        if (!_isGrounded && groundedThisFrame)
        {
            _stats.Acceleration = 1;
        }
        else if (groundedThisFrame)
        {
            _stats.Acceleration = PlayerStats.GroundAcceleration;
            _stats.Deceleration = PlayerStats.GroundDeceleration;
        }
        else
        {
            _stats.Acceleration = PlayerStats.AirAcceleration;
            _stats.Deceleration = PlayerStats.AirDeceleration;
        }
        _isGrounded = groundedThisFrame;
    }

    /// <summary>
    /// Sets the players velocity to 0 if its close enough to zero, used for setting idle state
    /// </summary>
    private void ResetVelocity()
    {
        if (_rb.velocity.magnitude <= 0.01f)
        {
            _rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Adds the force that moves the player horizontally.
    /// </summary>
    private void MovePlayer()
    {
        //Calculates the players target speed and sets the correct acceleration coefficient.
        float targetSpeed = _inputDirection * _stats.MovementSpeed;
        float coefficient = 0;
        if (Mathf.Abs(_rb.velocity.x) <= Mathf.Abs(targetSpeed))
        {
            coefficient = _stats.Acceleration;
        }
        else if (_stats.ApplyFriction)
        {
            coefficient = _stats.Deceleration;
        }
        /*
         * Calculates the difference in speed times the coefficient and adds the force,
         * causing the player never going above target speed and have responsive turning.
         */
        float speedDifference = (targetSpeed - _rb.velocity.x) / Time.fixedDeltaTime;
        float movement = coefficient * speedDifference;
        _rb.AddForce(Vector2.right * movement, ForceMode2D.Force);
    }

    /// <summary>
    ///  Applies force in the direction of <c>_stats.GravityDirection</c>.
    /// </summary>
    private void ApplyGravity()
    {
        if (_stats.ApplyGravity)
        {
            _rb.AddForce(_stats.GravityDirection * _stats.GravityScale, ForceMode2D.Force);
        }
    }
}
