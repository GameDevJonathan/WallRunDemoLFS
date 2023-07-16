using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallRunning : PlayerBaseState
{
    public PlayerWallRunning(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Reset();
        stateMachine.Animator.Play("WallRun");
        
    }

    public override void Exit()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        
    }
}
