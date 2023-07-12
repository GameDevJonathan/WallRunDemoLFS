using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;

    private Attack attack;
    private bool alreadyAppliedForce;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int AttackIndex) : base(stateMachine)
    {
        attack = stateMachine.Attacks[AttackIndex];
    }

    public override void Enter()
    {
        

        if (stateMachine.weaponL != null || stateMachine.weaponLL != null ||
            stateMachine.weaponR != null || stateMachine.weaponRL != null)
        {
            stateMachine.weaponL.SetAttack(attack.Damage, attack.KnockBack, attack.AttackType,attack.Stun);
            stateMachine.weaponR.SetAttack(attack.Damage, attack.KnockBack, attack.AttackType, attack.Stun);
            stateMachine.weaponLL.SetAttack(attack.Damage, attack.KnockBack, attack.AttackType,attack.Stun);
            stateMachine.weaponRL.SetAttack(attack.Damage, attack.KnockBack, attack.AttackType,attack.Stun);
        }
        //Debug.Log(attack.AnimationName);
        stateMachine.animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);

    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget(deltaTime);
        float normalizedTime = GetNormalizedTime(stateMachine.animator, "Attack");
        if (normalizedTime >= previousFrameTime && normalizedTime < 1)
        {
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }

            if (stateMachine.InputReader.AttackButtonPressed || stateMachine.InputReader.StrongAtkButtonPressed)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.targeter.currentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        previousFrameTime = normalizedTime;
    }


    public override void Exit()
    {

    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) { return; }
        if (normalizedTime < attack.ComboAttackTime) { return; }
        stateMachine.targeter.SelectClosestTarget();

        if (stateMachine.InputReader.AttackButtonPressed && attack.ComboStateIndex <= 4)
        {
            stateMachine.SwitchState(
                new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
        }

        if (stateMachine.InputReader.StrongAtkButtonPressed && (attack.ComboStateIndex > 5 && attack.ComboStateIndex <= 8))
        {
            stateMachine.SwitchState(
                new PlayerAttackingState(stateMachine, attack.ComboStateIndex));
        }
    }



    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }
        stateMachine.ForceReciever.AddForce(stateMachine.transform.forward * attack.Force);
        alreadyAppliedForce = true;
    }
}
