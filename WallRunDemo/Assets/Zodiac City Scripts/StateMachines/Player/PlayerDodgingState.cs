using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
    private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgeRightHash = Animator.StringToHash("DodgeRight");
    private float remainingDodgeTime;
    private const float CrossFadeDuration = 0.1f;

    private Vector3 dodgingDirectionInput;
    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }


    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeDuration;
        stateMachine.animator.SetFloat(DodgeRightHash, dodgingDirectionInput.y);
        stateMachine.animator.SetFloat(DodgeForwardHash, dodgingDirectionInput.x);
        stateMachine.animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossFadeDuration);

        stateMachine.Health.SetInvunerable(true);

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();
        movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        Move(movement, deltaTime);
        FaceTarget(deltaTime);

        remainingDodgeTime -= deltaTime;

        if(remainingDodgeTime <= 0)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvunerable(false);
    }
}
