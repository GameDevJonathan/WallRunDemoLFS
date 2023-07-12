using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParkourState : PlayerBaseState
{
    private const float CrossFadeDuration = 0.1f;
    string animName;
    private int ParkourHash;
    private bool Rotate;
    private Quaternion TargetRotation;
    private bool TargetMatching;
    private Vector3 MatchPos;
    private ParkourAction Action;
    private IEnumerator coroutine;

    public PlayerParkourState(PlayerStateMachine stateMachine, string animName, 
        bool rotate, Quaternion targetRotation, bool targetMatching, Vector3 matchPos,
        ParkourAction action) : base(stateMachine)
    {
        this.animName = animName;
        this.Rotate = rotate;
        this.TargetRotation = targetRotation;
        this.TargetMatching = targetMatching;
        this.MatchPos = matchPos;
        this.Action = action;
    }

    public override void Enter()
    {        
        stateMachine.animator.applyRootMotion = true;
        //coroutine = DoParkourAction(Action);
        //stateMachine.StartCoroutine(coroutine);
        ParkourHash = Animator.StringToHash(animName);
        stateMachine.animator.CrossFadeInFixedTime(ParkourHash, CrossFadeDuration);



    }


    public override void Tick(float deltaTime)
    {
        var animState = stateMachine.animator.GetNextAnimatorStateInfo(0);
        bool transition = stateMachine.animator.IsInTransition(0);

        float timer = 0f;
        while (timer <= animState.length)
        {
            timer += Time.deltaTime;

            if (Rotate)
                stateMachine.transform.rotation = Quaternion.RotateTowards(stateMachine.transform.rotation, TargetRotation, stateMachine.RotationSmoothValue * Time.deltaTime);

            if (TargetMatching && !transition)
                MatchTarget(Action);

         
        }


        if (GetNormalizedTime(stateMachine.animator,"Parkour") > 1)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

    }
    public override void Exit()
    {
        stateMachine.characterController.enabled = true;
        stateMachine.animator.applyRootMotion = false;
    }


    //IEnumerator DoParkourAction(ParkourAction action)
    //{
    //    ParkourHash = Animator.StringToHash(animName);
    //    stateMachine.animator.CrossFadeInFixedTime(ParkourHash, CrossFadeDuration);
    //    yield return null;

    //    var animState = stateMachine.animator.GetNextAnimatorStateInfo(0);
        
    //    float timer = 0f;
    //    while (timer <= animState.length)
    //    {
    //        timer += Time.deltaTime;

    //        if (Rotate)
    //            stateMachine.transform.rotation = Quaternion.RotateTowards(stateMachine.transform.rotation, TargetRotation, stateMachine.RotationSmoothValue * Time.deltaTime);

    //        if (TargetMatching)
    //            MatchTarget(Action);

    //        yield return null;
    //    }
    //}

    void MatchTarget(ParkourAction action)
    {
        if (stateMachine.animator.isMatchingTarget) return;
        stateMachine.characterController.enabled = false;
        stateMachine.animator.MatchTarget(action.MatchPos, stateMachine.transform.rotation,
           action.MatchBodyPart, new MatchTargetWeightMask(new Vector3(0, 1, 1), 0), action.MatchStartTime, action.MatchEndTime);
    }
}
