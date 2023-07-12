using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerBaseState
{
    private float previousFrameTime;
    private Skills skills;
    private bool alreadyAppliedForce;
    public PlayerSkillState(PlayerStateMachine stateMachine, int skillIndex) : base(stateMachine)
    {
        skills = stateMachine.Skills[skillIndex];
    }

    public override void Enter()
    {
        if (stateMachine.weaponL != null || stateMachine.weaponLL != null ||
           stateMachine.weaponR != null || stateMachine.weaponRL != null)
        {
            stateMachine.weaponL.SetAttack(skills.Damage, skills.KnockBack, skills.AttackType, 0);
            stateMachine.weaponR.SetAttack(skills.Damage, skills.KnockBack, skills.AttackType, 0);
            stateMachine.weaponLL.SetAttack(skills.Damage, skills.KnockBack, skills.AttackType, 0);
            stateMachine.weaponRL.SetAttack(skills.Damage, skills.KnockBack, skills.AttackType, 0);
        }
        //stateMachine.ForceReciever.Jump(15);        
        stateMachine.animator.CrossFadeInFixedTime(skills.AnimationName,skills.TransitionDuration);
    }
    public override void Tick(float deltaTime)
    {
        switch (skills.AnimationName)
        {
            case "RisingUpper":
            case "DragonBladeKick":
                stateMachine.animator.applyRootMotion = true;
                break;
        }

        if (GetNormalizedTime(stateMachine.animator, "Special") > 1)
        {
            //stateMachine.ForceReciever.Reset();
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.animator.applyRootMotion = false;
    }

    public void DragonWave()
    {
        stateMachine.Create(stateMachine.DragonWave, stateMachine.DragonWaveSpawnPoint);
    }
    //private void TryApplyForce()
    //{
    //    if (alreadyAppliedForce) { return; }
    //    stateMachine.ForceReciever.AddForce(stateMachine.transform.forward * skills.Force);
    //    stateMachine.ForceReciever.Jump(skills.UpForce);
    //    alreadyAppliedForce = true;
    //}

}
