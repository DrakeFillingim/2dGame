using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    public EntityStats ParentStats { get; set; }

    [SerializeField]
    private float _damage;
    /// <summary>
    /// The amount of health
    /// </summary>
    public float Damage {
        get => _damage;
        private set
        {
            _damage = value;
        }
    }

    [SerializeField]
    private float _staminaDamage;
    public float StaminaDamage { 
        get => _staminaDamage;
        private set 
        { 
            _staminaDamage = value;
        }
    }

    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("i also handle collisions now");
        if (other.TryGetComponent(out EntityStats collisionStats))
        {
            DamageHandler.Combat(this, collisionStats);
        }
    }

    public void EnableHitboxes()
    {
        _collider.enabled = true;
    }

    public void DisableHitboxes()
    {
        _collider.enabled = false;
    }

    public void SetAttackStats()
    {
        
    }
}
