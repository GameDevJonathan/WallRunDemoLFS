using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunState : EnemyBaseState
{
    private readonly int stunHash = Animator.StringToHash("Dizzy");
    private const float transitionDuration = 0.1f;

    public EnemyStunState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enemy Stun State");
        stateMachine.Animator.CrossFadeInFixedTime(stunHash, transitionDuration);
        Debug.Log("PlayingAnim");
    }
    public override void Tick(float deltaTime)
    {
        InputReader playerInput =
            GameObject.FindGameObjectWithTag("Player").GetComponent<InputReader>();
        if(playerInput != null)
        {
            if (playerInput.TakeDownInitiated)
            {
                Debug.Log("TAKENDOWN");
                FacePlayer();
                stateMachine.SwitchState(new EnemyTakeDownState(stateMachine));
                return;
            }
        }

    }

    public override void Exit()
    {
        
    }

}
