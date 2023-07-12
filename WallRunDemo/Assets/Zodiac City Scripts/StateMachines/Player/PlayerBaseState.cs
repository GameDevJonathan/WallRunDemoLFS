using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;


    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.characterController.Move((motion + stateMachine.ForceReciever.Movement) * deltaTime);
    }

    protected void FaceTarget(float deltaTime)
    {
        if (stateMachine.targeter.currentTarget == null) { return; }
        Vector3 lookPos = 
            stateMachine.targeter.currentTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;
        
        stateMachine.transform.rotation =
           Quaternion.Lerp(stateMachine.transform.rotation,
           Quaternion.LookRotation(lookPos),
           deltaTime * stateMachine.RotationSmoothValue);
    }

    protected void ReturnToLocomotion()
    {
        if (stateMachine.targeter.currentTarget != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }

    protected Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y  +
               right * stateMachine.InputReader.MovementValue.x;
    }

    protected void FaceMovement(Vector3 movement, float deltatime)
    {
        stateMachine.transform.rotation =
            Quaternion.Lerp(stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltatime * stateMachine.RotationSmoothValue);
    }


}
