using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    private readonly int DyingHash = Animator.StringToHash("Dying");
    private const float CrossFadeDuration = 0.1f;

    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(DyingHash, CrossFadeDuration);        
        stateMachine.weaponL.gameObject.SetActive(false);
        stateMachine.weaponR.gameObject.SetActive(false);
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
    
}
