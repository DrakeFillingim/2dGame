using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    [SerializeField]
    private WeaponStats[] _allWeapons;

    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();

        EntityStats parentStats = GetComponent<EntityStats>();
        foreach (var weapon in _allWeapons)
        {
            weapon.ParentStats = parentStats;
        }
    }

    public void StartAttack(int animationHash)
    {
        _animator.Play(animationHash);
    }

    public void OnHitBoxEnable(int index)
    {
        _allWeapons[index].EnableHitboxes();
    }

    public void OnHitBoxDisable(int index)
    {
        _allWeapons[index].DisableHitboxes();
    }

    public void OnAttackFinish()
    {
        
    }

    /*private void OnAttackHit(Collider2D collision)
    {
        if (collision.TryGetComponent(out EntityStats collisionStats))
        {
            DamageHandler.Combat(this, collisionStats);
        }
    }*/
}
