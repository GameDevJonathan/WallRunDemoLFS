using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    private string deathIndex;
    private readonly int DeadHighHash = Animator.StringToHash("DeadHigh");
    private const float TransitionDuration = 0.1f;
    public EnemyDeadState(EnemyStateMachine stateMachine, string index = "") : base(stateMachine)
    {
        deathIndex = index;
    }

    public override void Enter()
    {
        //stateMachine.Ragdoll.ToggleRagdoll(true);
        Debug.Log("DeadState");
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);
        stateMachine.Health.SetInvunerable(true);

        Debug.Log(deathIndex + "dead state");
        switch (deathIndex)
        {
            case "High":
                stateMachine.Animator.CrossFade(DeadHighHash, TransitionDuration);
                stateMachine.Animator.applyRootMotion = true;
                break;
            default:
                break;
        }


    }

    public override void Tick(float deltaTime)
    {
        if(GetNormalizedTime(stateMachine.Animator,"DeadHigh") > 1f)
        {
            stateMachine.Animator.applyRootMotion = false;
        }
    }

    public override void Exit()
    {

    }

}
