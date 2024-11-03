using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public const float GroundAcceleration = 0.2f;
    public const float GroundDeceleration = 0.175f;
    public const float AirAcceleration = 0.075f;
    public const float AirDeceleration = 0.05f;

    public float Acceleration { get; set; } = GroundAcceleration;
    public float Deceleration { get; set; } = GroundDeceleration;

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

    public float GravityScale { get; set; } = 30;
    public Vector2 GravityDirection { get; set; } = Vector2.down;
}
