using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("Block");
    private const float CrossFadeDuration = 0.1f;
    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(BlockHash, CrossFadeDuration);
        stateMachine.Health.SetInvunerable(true);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget(deltaTime);

        if (!stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            return;
        }

        if(stateMachine.targeter.currentTarget == null && !stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvunerable(false);
    }    
}
