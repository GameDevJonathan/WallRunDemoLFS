using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    int dodgeState;
    private readonly int NeutralDodgeHash = Animator.StringToHash("RockAwayBwd");
    private readonly int DodgeForwardHash = Animator.StringToHash("DodgeFwd");
    //private readonly int DodgeForwardHash = Animator.StringToHash("Dodge_Fwd_Root");
    private float AnimatorDampTime = 0.05f;

    public PlayerDodgeState(PlayerStateMachine stateMachine, int dodgeState) : base(stateMachine)
    {
        this.dodgeState = dodgeState;
    
    }

    public override void Enter()
    {
        stateMachine.Health.SetInvunerable(true);
        switch (dodgeState)
        {
            case 0:
                stateMachine.animator.CrossFadeInFixedTime(NeutralDodgeHash, AnimatorDampTime);
                break;
            case 1:
                stateMachine.animator.CrossFadeInFixedTime(DodgeForwardHash, AnimatorDampTime);
                stateMachine.animator.applyRootMotion = true;
                break;
            default:
                break;
        }
    }
    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.animator,"Dodge") < 1f) { return; }
        stateMachine.animator.applyRootMotion = false;
        ReturnToLocomotion();
        
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvunerable(false);
    }

}
