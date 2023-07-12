using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");
    private const float CrossFadeDuration = 0.1f;



    public override void Enter()
    {
        //Debug.Log("Targeting State");
        stateMachine.InputReader.CancelEvent += OnCancel;

        stateMachine.InputReader.DodgeEvent += OnDodge;

        stateMachine.InputReader.JumpEvent += OnJump;

        stateMachine.animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDuration);
    }
    public override void Tick(float deltaTime)
    {
        stateMachine.targeter.CycleTarget();        
        
        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 1));
            return;
        }

        if (stateMachine.targeter.currentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = TargetCalculateMovement();

        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget(deltaTime);

    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;

    }

    private void OnCancel()
    {
        stateMachine.targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private void OnDodge()
    {
        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.MovementValue));


        //if(Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCoolDown) { return; }

        //stateMachine.SetDodgeTime(Time.time);
        //dodgingDirectionInput = stateMachine.InputReader.MovementValue;
        //remainingDodgeTime = stateMachine.DodgeDuration;
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    private Vector3 TargetCalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.animator.SetFloat(TargetingForwardHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.animator.SetFloat(TargetingForwardHash, value, 0.1f, deltaTime);
        }

        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            stateMachine.animator.SetFloat(TargetingRightHash, value, 0.1f, deltaTime);
        }
    }

}
