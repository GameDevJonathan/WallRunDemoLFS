using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Player { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float DetectionRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int AttackKnockback { get; private set; }
    [field: SerializeField] public string AttackType { get; private set; }




    protected EnemyBaseState baseState;
    public enum EnemyState { Idle, Chase, Attack, Wait, KnockDown }
    public EnemyState state;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }



    private void OnEnable()
    {
        
        Health.OnTakeDamage += HandleDamage;
        Health.OnDie += HandleDeath;
    }

    private void OnDisable()
    {
       
        Health.OnTakeDamage -= HandleDamage;
        Health.OnDie -= HandleDeath;
    }
    
    private void HandleDamage()
    {
        if (Health.type != "KnockDown")
        {
            SwitchState(new EnemyImpactState(this, Health.type));
            return;
        }
        else if (Health.type == "KnockDown")
        {
            Debug.Log("Switch state to knock Down");
            Debug.Log("HandleDamage Method");
            SwitchState(new EnemyKnockDownState(this));
            return;
        }
    }
    private void HandleDeath()
    {
        if (Health.type == "KnockDown") { return; }
        SwitchState(new EnemyDeadState(this, Health.type));
        return;
    }

    private void HandleStun()
    {
        Debug.Log("Here");
        SwitchState(new EnemyStunState(this));
        return;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }
}
