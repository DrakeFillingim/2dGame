using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    /*public const float WalkSpeed = 5;
    public const float CrouchSpeed = 3;
    public const float RunSpeed = 12.5f;*/

    public const float GroundAcceleration = 0.2f;
    public const float GroundDeceleration = 0.175f;
    public const float AirAcceleration = 0.075f;
    public const float AirDeceleration = 0.05f;

    public float Acceleration { get; set; } = GroundAcceleration;
    public float Deceleration { get; set; } = GroundDeceleration;
    public bool ApplyFriction { get; set; } = true;

    public const float AttackTapTime = 0.2f;
    public const float AttackChargeTime = 1f;

    private const float _baseSpeed = 5;
    private float _speedModifier = 1;
    public float MovementSpeed
    {
        get => _baseSpeed * _speedModifier;
        set
        {
            _speedModifier = value / _baseSpeed;
        }
    }

    public const int MaxDashes = 2;
    public int CurrentDashes { get; set; } = 0;

    public const float DashCooldown = .5f;
    public bool IsDashing { get; set; } = false;
    public Timer CanDash { get; private set; }

    public bool ApplyGravity { get; set; } = true;
    public float GravityScale { get; set; } = 50;
    public Vector2 GravityDirection { get; set; } = Vector2.down;

    public void Start()
    {
        CanDash = Timer.CreateTimer(gameObject, "Dash Cooldown", () => { }, DashCooldown);
    }
}
