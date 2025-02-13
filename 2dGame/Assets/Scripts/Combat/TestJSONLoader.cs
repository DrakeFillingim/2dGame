using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestJSONLoader : MonoBehaviour
{
    private TextAsset _jsonText;
    private Dictionary<string, AttackData> _attackDictionary;

    // Start is called before the first frame update
    void Start()
    {
        _jsonText = Resources.Load<TextAsset>("AttackData");

        AttackListJSON j = JsonUtility.FromJson<AttackListJSON>(_jsonText.text);
        _attackDictionary = j.Attacks.ToDictionary(x => x.Name, x => x);
        foreach (var k in _attackDictionary.Keys)
        {
            Debug.Log(k);
        }
        Debug.Log(_attackDictionary["Attack 1"].Weapons[0]);
    }
}

[Serializable]
public class AttackListJSON
{
    public AttackData[] Attacks;
}

[Serializable]
public class AttackData
{
    public string Name;
    public WeaponData[] Weapons;
}

[Serializable]
public class WeaponData
{
    public string Name;
    public float Damage;
    public float StaminaDamage;
    public float ParryDamage;
}

// Toplevel holds an array of every attack
// AttackJson stores the name of the attack, and all the weapons used in it
// WeaponJson holds the name of the weapon, and its associated damages

// State ->(Attack Name) AttackComponent ->(Damages) Weapon
