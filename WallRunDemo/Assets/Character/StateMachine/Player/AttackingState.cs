using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingState : PlayerBaseState
{
    Attacks attack;
    
    public AttackingState(PlayerStateMachine stateMachine, int attackID) : base(stateMachine)
    {
        attack = stateMachine.Attacks[attackID];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName,.1f);
        
    }
    public override void Tick(float deltaTime)
    {
        
        
    }

    public override void Exit()
    {
        
    }


   
}
