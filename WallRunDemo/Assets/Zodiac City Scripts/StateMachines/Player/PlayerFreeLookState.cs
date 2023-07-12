using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");
    public float AnimatorDampTime = 0.05f;
    private float freeLookValue = 1;
    private float freeLookMoveSpeed;
    private bool shouldFade;
    private const float CrossFadeDuration = 0.3f;

    public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
    {
        this.shouldFade = shouldFade;
        this.freeLookMoveSpeed = stateMachine.FreeLookMovementSpeed;
    }
    public override void Enter()
    {
        stateMachine.InputReader.AttackEvent += OnAttack;
        stateMachine.InputReader.StrongAttackEvent += OnStrongAttack;
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.animator.SetFloat(FreeLookSpeedHash, 0);


        if (shouldFade)
        {
            stateMachine.animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, CrossFadeDuration);
        }
        else
        {
            stateMachine.animator.Play(FreeLookBlendTreeHash);
        }

    }
    public override void Tick(float deltaTime)
    {
        var hitdata = stateMachine.EnviormentScaner.ObstacleCheck();

        if (stateMachine.InputReader.AttackButtonPressed && stateMachine.InputReader.ReadySkill)
        {
            Debug.Log("Attack pressed");
            //SpecialAttack(2);
            //return;

        }

        if (stateMachine.InputReader.StrongAtkButtonPressed && stateMachine.InputReader.ReadySkill)
        {
            Debug.Log("Strong Attack Pressed");
            SpecialAttack(2);
            return;
        }

        if (stateMachine.InputReader.JumpButtonPressed)
        {
            if (stateMachine.InputReader.ReadySkill)
            {
                Debug.Log("Jump pressed");
                stateMachine.targeter.SelectClosestTarget();
                SpecialAttack(1);
                return;
            }

            //if (hitdata.forwardHitFound)
            //{
            //    foreach (var action in stateMachine.ParkourActions)
            //    {
            //        if (action.CheckIfPossible(hitdata, stateMachine.transform))
            //        {
            //            Debug.Log("Obstacle Found " + hitdata.forwardHit.transform.name);
            //            stateMachine.SwitchState(new PlayerParkourState(stateMachine,
            //                action.AnimName,action.RotateToObstacle,action.TargetRotation,
            //                action.EnableTargetMatching,action.MatchPos, action));
            //            return;
            //        }
            //    }
            //}


        }

        if (stateMachine.targeter.canTakeDown && stateMachine.InputReader.TakeDownInitiated)
        {
            OnTakeDown();
            return;
            
        }
        //[To Do] Dodge Button Action

        //Blocking
        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }


        if (stateMachine.InputReader.DodgeButtonPressed && stateMachine.InputReader.ReadySkill)
        {
            Debug.Log("Dodge Button Pressed");
            SpecialAttack(3);
            return;
        }



        if (stateMachine.characterController.velocity.y < 0f && !stateMachine.characterController.isGrounded)
        {
            stateMachine.SwitchState(new PlayerFallState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();


        //if i'm holding the modified button do said action based on if i found hit data
        if (stateMachine.InputReader.modified)
        {
            if (hitdata.forwardHitFound)
            {
                foreach (var action in stateMachine.ParkourActions)
                {
                    if (action.CheckIfPossible(hitdata, stateMachine.transform))
                    {
                        Debug.Log("Obstacle Found " + hitdata.forwardHit.transform.name);
                        stateMachine.SwitchState(new PlayerParkourState(stateMachine,
                            action.AnimName, action.RotateToObstacle, action.TargetRotation,
                            action.EnableTargetMatching, action.MatchPos, action));
                        return;
                    }
                }
            }


            if (!hitdata.forwardHitFound)
            {
                freeLookValue = 2;
                freeLookMoveSpeed = 6;
            }
        }
        else //if i release the button return to normal 
        {

            freeLookValue = Mathf.Clamp(
                Mathf.Abs(stateMachine.InputReader.MovementValue.x) +
                Mathf.Abs(stateMachine.InputReader.MovementValue.y), 0f, 1f);

            freeLookMoveSpeed = stateMachine.FreeLookMovementSpeed;
        }

        Move(movement * freeLookMoveSpeed, deltaTime);

        //stateMachine.transform.Translate(movement*deltaTime);
        //stateMachine.characterController.Move(movement * deltaTime * freeLookMoveSpeed);

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;

        }

        stateMachine.animator.SetFloat(FreeLookSpeedHash, freeLookValue, AnimatorDampTime, deltaTime);
        FaceMovement(movement, deltaTime);

    }


    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= OnAttack;
        stateMachine.InputReader.StrongAttackEvent -= OnStrongAttack;
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
    }

    private void OnTakeDown()
    {
        Debug.Log("Almost There");
        stateMachine.targeter.SelectClosestTarget();
        stateMachine.SwitchState(new PlayerTakeDownState(stateMachine));
        return;
    }

    private void OnDodge()
    {
        if (stateMachine.InputReader.ReadySkill) { return; }
        Debug.Log("State Machine takedown " + stateMachine.targeter.canTakeDown);
        if (stateMachine.targeter.canTakeDown) { return; }

        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.SwitchState(new PlayerDodgeState(stateMachine, 0));
        }
        else
        {
            stateMachine.SwitchState(new PlayerDodgeState(stateMachine, 1));
        }
    }

    private void OnAttack()
    {
        if (stateMachine.InputReader.ReadySkill) { return; }
        stateMachine.targeter.SelectClosestTarget();
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 1));
    }

    private void OnStrongAttack()
    {
        if (stateMachine.InputReader.ReadySkill) { return; }
        if (stateMachine.targeter.canTakeDown) { return; }

        stateMachine.targeter.SelectClosestTarget();
        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 5));
    }

    private void OnJump()
    {

        if (stateMachine.InputReader.ReadySkill) { return; }
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    private void OnTarget()
    {
        if (!stateMachine.targeter.SelectTarget()) { return; }
        stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
    }

    private void SpecialAttack(int ind)
    {
        stateMachine.targeter.SelectClosestTarget();
        stateMachine.SwitchState(new PlayerSkillState(stateMachine, ind));

    }
}
