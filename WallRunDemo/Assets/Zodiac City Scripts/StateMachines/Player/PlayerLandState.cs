using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerBaseState
{
    private readonly int LandHash = Animator.StringToHash("Landing");
    
    public PlayerLandState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.animator.Play(LandHash);
    }

    public override void Tick(float deltaTime)
    {
        
        
        if(stateMachine.InputReader.MovementValue != Vector2.zero)
        {
            ReturnToLocomotion();
        }
        
        if(GetNormalizedTime(stateMachine.animator, "Landing") < 1f) { return; }
        ReturnToLocomotion();
        

    }

    public override void Exit()
    {

    }


}
