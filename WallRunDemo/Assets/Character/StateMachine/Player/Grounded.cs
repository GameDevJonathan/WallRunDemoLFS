using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("Movement");
    public float AnimatorDampTime = 0.1f;
    private float freeLookValue = 1;
    private float freeLookMoveSpeed;
    //private bool shouldFade;
    private const float CrossFadeDuration = 0.3f;

    public Grounded(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        this.freeLookMoveSpeed = stateMachine.FreeLookMovementSpeed;
    }

    public override void Enter()
    {
        stateMachine.Animator.Play(FreeLookBlendTreeHash);
        
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;

        }

        Vector3 movement = CalculateMovement();
        Move(movement * freeLookMoveSpeed, deltaTime);
        
        stateMachine.Animator.SetFloat(FreeLookSpeedHash,freeLookValue,AnimatorDampTime,deltaTime);
        FaceMovement(movement, deltaTime);



    }

    public override void Exit()
    {
        
    }

    protected Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Debug.Log(stateMachine.InputReader.MovementValue.y);
        Debug.Log(stateMachine.InputReader.MovementValue.x);

        return forward * stateMachine.InputReader.MovementValue.y +
               right * stateMachine.InputReader.MovementValue.x;
    }
}
