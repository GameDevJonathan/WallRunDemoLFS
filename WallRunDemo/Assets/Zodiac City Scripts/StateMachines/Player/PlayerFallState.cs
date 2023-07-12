using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Falling");
    private const float CrossFadeDuration = 0.3f;
    private Vector3 Momentum;
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        Momentum = stateMachine.characterController.velocity;
        Momentum.y = 0f;
        stateMachine.animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
        stateMachine.LedgeDetector.onLedgeDetect += HandleLedgeDetect;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        //Move(Momentum, deltaTime);
        Move(movement + Momentum, deltaTime);
        FaceTarget(deltaTime);
        if(movement != Vector3.zero && !stateMachine.targeter.currentTarget) 
            FaceMovement(movement, deltaTime);

        if (stateMachine.characterController.isGrounded)
        {
            stateMachine.SwitchState(new PlayerLandState(stateMachine));
        }

    }

    public override void Exit()
    {
        stateMachine.LedgeDetector.onLedgeDetect -= HandleLedgeDetect;

    }

    private void HandleLedgeDetect(Vector3 closestPoint, Vector3 ledgeForward)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine, closestPoint, ledgeForward));
    }

}
