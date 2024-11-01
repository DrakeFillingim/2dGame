using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public const float Acceleration = 0.2f;
    public const float Deceleration = 0.175f;

    public const float AttackTapTime = 0.2f;
    public const float AttackChargeTime = 1f;

    private const float _baseSpeed = 3;
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
