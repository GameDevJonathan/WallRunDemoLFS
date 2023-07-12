using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine){}
    //private float Speed = 1f;
    private readonly int Locomotion = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    public float AnimatorDampTime = 0.05f;
    private const float CrossFadeDuration = 0.1f;
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(Locomotion, CrossFadeDuration);
        //Debug.Log("Chasing State");
    }

    public override void Tick(float deltaTime)
    {       
        if (!IsInChaseRange())
        {
            //Debug.Log("Not in Chase Range");
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }else if (IsInAttackRange())
        {
            //Debug.Log("In Attack Range");
            Debug.Log("Remember to switch back to attack state");
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            //stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }
        MoveToPlayer(deltaTime);
        FacePlayer();
        stateMachine.Animator.SetFloat(SpeedHash,1f, AnimatorDampTime, deltaTime);

       
        
    }

    private bool IsInAttackRange()
    {
        if (stateMachine.Player.isDead) { return false; }

        float playerDistanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.AttackRange * stateMachine.AttackRange;
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        //if (stateMachine.Agent.isOnNavMesh)
        //{
            stateMachine.Agent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
            
            stateMachine.Agent.velocity = stateMachine.Controller.velocity;
            
        //}
        
    }
}