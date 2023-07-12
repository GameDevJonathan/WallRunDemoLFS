using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDownState : PlayerBaseState
{
    private readonly int SuplexHash = Animator.StringToHash("SuplexAtt");
    private const float CrossFadeDuration = 0.1f;
    Target currentTarget;
    public PlayerTakeDownState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        currentTarget = stateMachine.targeter.currentQuickTarget.GetComponent<Target>();    
    }

    public override void Enter()
    {
        //stateMachine.characterController.detectCollisions = false;
        stateMachine.animator.CrossFadeInFixedTime(SuplexHash, CrossFadeDuration);
        if(currentTarget != null)
        {
            Debug.Log("Got Anim");
        }
        stateMachine.animator.applyRootMotion = true;
        
    }
    public override void Tick(float deltaTime)
    {
        float normalizedTimed = GetNormalizedTime(stateMachine.animator, "TakeDown");
        if(normalizedTimed > 1f)
        {
            stateMachine.characterController.detectCollisions = true;
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, true));
            stateMachine.animator.applyRootMotion = false;
        }

        
    }

    public override void Exit()
    {
        
    }


    
}
