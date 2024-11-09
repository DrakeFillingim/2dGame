using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChargeAttack : State
{


    public override void OnStart()
    {
        _inputMap["ChargeAttack"].performed += OnChargeAttackPerformed;
    }

    public override void OnUpdate()
    {

    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        _inputMap["ChargeAttack"].performed -= OnChargeAttackPerformed;
    }

    private void OnChargeAttackPerformed(InputAction.CallbackContext context)
    {

    }
}
