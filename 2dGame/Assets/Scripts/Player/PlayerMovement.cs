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
        if (MovementHelper.IsGrounded(gameObject, _stats.GravityDirection))
        {
            _stats.Acceleration = PlayerStats.GroundAcceleration;
            _stats.Deceleration = PlayerStats.GroundDeceleration;
        }
        else
        {
            _stats.Acceleration = PlayerStats.AirAcceleration;
            _stats.Deceleration = PlayerStats.AirDeceleration;
        }
        ResetVelocity();
        MovePlayer();
        ApplyGravity();
    }

    private void OnDisable()
    {
        _inputMap["Move"].performed -= OnMove;
    }

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

    private void ResetVelocity()
    {
        if (_rb.velocity.magnitude <= 0.01f)
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void MovePlayer()
    {
        float targetSpeed = _inputDirection * _stats.MovementSpeed;
        float speedDifference = (targetSpeed - _rb.velocity.x) / Time.fixedDeltaTime;
        float coefficient = (Mathf.Abs(_rb.velocity.x) < Mathf.Abs(targetSpeed)) ? _stats.Acceleration : _stats.Deceleration;
        float movement = coefficient * speedDifference;
        _rb.AddForce(Vector2.right * movement, ForceMode2D.Force);
    }

    private void ApplyGravity()
    {
        if (_stats.ApplyGravity)
        {
            _rb.AddForce(_stats.GravityDirection * _stats.GravityScale, ForceMode2D.Force);
        }
    }
}
