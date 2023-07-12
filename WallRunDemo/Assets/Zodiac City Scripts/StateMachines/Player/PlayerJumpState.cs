using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private const float CrossFadeDuration = 0.1f;
    private Vector3 Momentum;
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.ForceReciever.Jump(stateMachine.JumpForce);
        Momentum = stateMachine.characterController.velocity;
        Momentum.y = 0f;
        stateMachine.animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);
        stateMachine.LedgeDetector.onLedgeDetect += HandleLedgeDetect;
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        //Move(Momentum, deltaTime);
        Move(movement + Momentum, deltaTime);
        FaceTarget(deltaTime);

        if (movement != Vector3.zero && !stateMachine.targeter.currentTarget)
        {
            FaceMovement(movement, deltaTime);
        }

        if (stateMachine.characterController.velocity.y <= 0f)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
            return;
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
