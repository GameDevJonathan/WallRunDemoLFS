using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandState : PlayerBaseState
{
    Vector2 inputMovement;
    private readonly int LandHash = Animator.StringToHash("JumpEnd");
    private readonly int RollingLandHash = Animator.StringToHash("LandingRoll");
    private const float CrossFadeDuration = 0.3f;

    public PlayerLandState(PlayerStateMachine stateMachine, Vector2 inputMovement) : base(stateMachine)
    {
        this.inputMovement = stateMachine.InputReader.MovementValue;

    }

    public override void Enter()
    {
        if (inputMovement != Vector2.zero) 
        {
            Debug.Log("Rolling Land");
            stateMachine.Animator.CrossFadeInFixedTime(RollingLandHash,0.01f);
            stateMachine.Animator.applyRootMotion = true;
        }
        else if (inputMovement == Vector2.zero)
        {
            stateMachine.Animator.CrossFadeInFixedTime(LandHash, CrossFadeDuration);

        }
    }

    public override void Tick(float deltaTime)
    {
        //if (stateMachine.Animator.GetCurrentAnimatorStateInfo(0).IsName("LandingRoll"))
        //{
        //    Debug.Log("True");
        //}

        if (GetNormalizedTime(stateMachine.Animator, "Landing") < 1f) { return; }
        ReturnToLocomotion();

    }

    public override void Exit()
    {

    }


}
