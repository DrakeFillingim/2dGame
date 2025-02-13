using UnityEngine;

/// <summary>
/// For tracking stats that every single entity in the game will have.
/// Contains events for flinching and staggering.
/// </summary>
public abstract class EntityStats : MonoBehaviour
{
    public event System.Action Flinch;
    public event System.Action Stagger;
    public event System.Action Parried;

    [SerializeField]
    private float _health;
    [SerializeField]
    private float _maxHealth;
    public float Health
    {
        get => _health;
        set => _health = Mathf.Min(value, _maxHealth);
    }

    private float _poise;
    private float _maxPoise;
    public float Poise
    {
        get => _poise;
        set
        {
            if (value > _maxPoise)
            {
                _poise = _maxPoise;
                Flinch?.Invoke();
                return;
            }
            _poise = value;
        }
    }

    [SerializeField]
    private float _stamina;
    [SerializeField]
    private float _maxStamina;
    public float Stamina
    {
        get => _stamina;
        set
        {
            if (value > _maxStamina)
            {
                _stamina = _maxStamina;
                Stagger?.Invoke();
                return;
            }
            _stamina = value;
        }
    }

    public DamageHandler.CombatStates CombatState { get; set; } = DamageHandler.CombatStates.Undefended;

    /*protected void RaiseFlinch()
    {
        Flinch?.Invoke();
    }

    protected void RaiseStagger()
    {
        Stagger?.Invoke();
    }*/

    public abstract void OnParried(float staminaDamage);

    public abstract void OnParryAttack(float staminaDamage);

    public abstract void OnBlockAttack(float staminaDamage);

    public abstract void OnHitByAttack(float healthDamage, float staminaDamage);

    protected void RaiseParried()
    {
        Parried?.Invoke();
    }
}
