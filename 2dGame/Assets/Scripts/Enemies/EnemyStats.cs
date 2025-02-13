using UnityEngine;

public class EnemyStats : EntityStats
{
    public override void OnParried(float staminaDamage)
    {
        Stamina += staminaDamage;
        RaiseParried();
    }

    public override void OnParryAttack(float staminaDamage)
    {
        Stamina += staminaDamage;
    }

    public override void OnBlockAttack(float staminaDamage)
    {
        Stamina += staminaDamage;
    }

    public override void OnHitByAttack(float healthDamage, float staminaDamage)
    {
        Health -= healthDamage;
        Stamina += staminaDamage;
    }
}
