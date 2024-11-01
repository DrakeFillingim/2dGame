using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    private PlayerStats _stats;
    private Rigidbody2D _rb;

    void Start()
    {
        _stats = GetComponent<PlayerStats>();
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!MovementHelper.IsGrounded(gameObject, _stats.GravityDirection))
        {
            _rb.AddForce(_stats.GravityDirection * _stats.GravityScale, ForceMode2D.Force);
            print("added force");
        }
    }
}
