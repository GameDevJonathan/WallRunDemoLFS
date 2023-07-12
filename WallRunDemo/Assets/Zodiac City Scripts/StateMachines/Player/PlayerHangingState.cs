using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private readonly int HangingHash = Animator.StringToHash("Hanging");
    private const float CrossFadeDuration = 0.1f;
    private Vector3 closestPoint;
    private Vector3 ledgeForward;
    public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 closestPoint, Vector3 ledgeForward) : base(stateMachine)
    {
        this.closestPoint = closestPoint;
        this.ledgeForward = ledgeForward;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);
        stateMachine.characterController.enabled = false;
        stateMachine.transform.position = closestPoint - (stateMachine.LedgeDetector.transform.position - stateMachine.transform.position);
        stateMachine.characterController.enabled = true;
        stateMachine.animator.CrossFadeInFixedTime(HangingHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.MovementValue.y < 0f)
        {
            stateMachine.characterController.Move(Vector3.zero);
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
            stateMachine.ForceReciever.Reset();
        }else if(stateMachine.InputReader.MovementValue.y > 0f)
        {
            stateMachine.SwitchState(new PlayerClimbingState(stateMachine));
        }
        
    }

    public override void Exit()
    {
        
    }  

}
