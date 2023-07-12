using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    //private readonly int ImpactHash = Animator.StringToHash("Impact");
    private int ImpactHash;
    private const float TransitionDuration = 0.1f;
    private string type;
    const string HighLeft = "HighLeft";
    const string HighRight = "HighRight";
    const string MidLeft = "MidLeft";
    const string MidRight = "MidRight";

    private float duration = 1f;

    public EnemyImpactState(EnemyStateMachine stateMachine, string type) : base(stateMachine)
    {
        this.type = type;
    }

    public override void Enter()
    {
        if (type == "KnockDown")
        {
            Debug.Log(stateMachine.transform.name + " " + type);
        }
        switch (type)
        {
            case HighLeft:
                ImpactHash = Animator.StringToHash("HighLeft");
                stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);
                break;
            case HighRight:
                ImpactHash = Animator.StringToHash("HighRight");
                stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, TransitionDuration);
                break;
            default:
                break;
        }
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (stateMachine.Health.isStunned)
        {
            if (GetNormalizedTime(stateMachine.Animator, type) > 1f)
                stateMachine.SwitchState(new EnemyStunState(stateMachine));
            return;
        }
        else
        {
            if (GetNormalizedTime(stateMachine.Animator, type) > 1f)
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {

    }
}
