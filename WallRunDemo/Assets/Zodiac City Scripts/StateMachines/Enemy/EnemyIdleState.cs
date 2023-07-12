using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine){}

    //private float Speed = 0f;
    private readonly int Locomotion = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    public float AnimatorDampTime = 0.05f;
    private const float CrossFadeDuration = 0.1f;
    

    public override void Enter()
    {
       stateMachine.Animator.CrossFadeInFixedTime(Locomotion,CrossFadeDuration);        
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (IsInChaseRange())
        {
            //Debug.Log("in range");
            //stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        //FacePlayer();
        stateMachine.Animator.SetFloat(SpeedHash,0f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        
    }


   
}
