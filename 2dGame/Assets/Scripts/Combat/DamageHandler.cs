using UnityEngine;

/// <summary>
/// 
/// </summary>
public static class DamageHandler
{
    private const float ParryRatio = .1f;
    private const float ParryDamage = 1f;

    public enum CombatStates
    {
        Undefended,
        Blocking,
        Parrying,
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    public static void Combat(WeaponStats attack, EntityStats defendStats)
    {
        var attackerStats = attack.ParentStats;

        // Blocking or Undefended deals same stamina damage, parrying does 1/10 for testing?
        
        switch (defendStats.CombatState)
        {
            case CombatStates.Parrying:
                attackerStats.OnParried(ParryDamage);
                defendStats.OnParryAttack(attack.StaminaDamage * ParryRatio);
                break;
            case CombatStates.Blocking:
                defendStats.OnBlockAttack(attack.StaminaDamage);
                break;
            case CombatStates.Undefended:
                defendStats.OnHitByAttack(attack.Damage, attack.StaminaDamage);
                break;
        }
    }
}
