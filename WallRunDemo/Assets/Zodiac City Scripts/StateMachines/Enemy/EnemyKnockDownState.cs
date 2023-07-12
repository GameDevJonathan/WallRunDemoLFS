using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockDownState : EnemyBaseState
{
    private readonly int KnockDownHash = Animator.StringToHash("KnockDown");
    private readonly int KipUpHash = Animator.StringToHash("KipUp");
    private const float TransitionDuration = 0.1f;
    private float DownTime;
    private float DownTimeCounter;
    private bool OnFloor;
    public EnemyKnockDownState(EnemyStateMachine stateMachine, bool onFloor = false) : base(stateMachine)
    {
        this.OnFloor = onFloor;
    }

    public override void Enter()
    {
        Debug.Log("KnockDown State");
        stateMachine.Animator.CrossFadeInFixedTime(KnockDownHash, TransitionDuration);
        stateMachine.Health.SetInvunerable(true);
        DownTime = Random.Range(1f, 3f);
        DownTimeCounter = DownTime;
        //Debug.Log(DownTime);
    }
    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        DownTimeCounter = Mathf.Max(DownTimeCounter - deltaTime, 0);
        //Debug.Log(DownTimeCounter);

        if(GetNormalizedTime(stateMachine.Animator,"KnockDown") >= 1 && DownTimeCounter == 0)
        {
            //Debug.Log("Play Next Anim");
            if (!stateMachine.Health.isDead)
            {
                stateMachine.Animator.Play(KipUpHash);
            }
            else
            {
                stateMachine.SwitchState(new EnemyDeadState(stateMachine));
            }
        }

        if (GetNormalizedTime(stateMachine.Animator, "KipUp") >= 1)
        {
            //Debug.Log("Play Next Anim");
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.Health.SetInvunerable(false);
    }

}
