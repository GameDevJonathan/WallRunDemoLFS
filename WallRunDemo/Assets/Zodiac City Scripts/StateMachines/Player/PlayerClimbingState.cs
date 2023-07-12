using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbingState : PlayerBaseState
{
    private readonly int PullUpHash = Animator.StringToHash("PullUp");
    private readonly Vector3 offset = new Vector3(0f, 3.235f, 0.65f);
    private const float CrossFadeDuration = 0.1f;
    public PlayerClimbingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.CrossFadeInFixedTime(PullUpHash, CrossFadeDuration);

        
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.animator,"Climb") < 1f) { return; }
        stateMachine.characterController.enabled = false;
        stateMachine.transform.Translate(offset, Space.Self);
        stateMachine.characterController.enabled = true;
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine,false));        

    }

    public override void Exit()
    {
        stateMachine.characterController.Move(Vector3.zero);
        stateMachine.ForceReciever.Reset();

    }
}
