using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Damage");
    private const float TransitionDuration = 0.1f;
    private float duration = 1f;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);        
    }   

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if ( stateMachine.animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f )
        {
            //Debug.Log(stateMachine.animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            return; 
        }
        ReturnToLocomotion();
        
        
    }

    public override void Exit()
    {
        
    }
}
