using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDownState : EnemyBaseState
{
    private readonly int SuplexHash = Animator.StringToHash("SuplexVic");
    private const float CrossFadeDuration = 0.1f;
    public EnemyTakeDownState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //stateMachine.Controller.detectCollisions = false;
        stateMachine.Animator.CrossFadeInFixedTime(SuplexHash, CrossFadeDuration);
        stateMachine.Animator.applyRootMotion = true;
    }
    
    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.Animator,"TakeDown") > 1)
        {
            stateMachine.Controller.detectCollisions = true;
            stateMachine.Animator.applyRootMotion = false;
        }
    }

    public override void Exit()
    {
        
    }

}
