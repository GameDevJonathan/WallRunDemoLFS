using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] private CinemachineTargetGroup cineTargetGroup;

    [SerializeField] private List<Target> targets = new List<Target>();

    [SerializeField] private SphereCollider collider;
    [SerializeField] private LayerMask enemyMask;
    [field: SerializeField] private PlayerStateMachine stateMachine { get; set; }
    [SerializeField] private int index = 0;
    [SerializeField] private bool didCycle = false;
    [SerializeField] public bool canTakeDown;

    [field: SerializeField] public Target currentTarget { get; private set; }
    [field: SerializeField] public Target currentQuickTarget { get; private set; }
    //[field: SerializeField] public Target closestTarget { get; private set; }


    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        AnimatorStateInfo anim = stateMachine.animator.GetCurrentAnimatorStateInfo(0);
        if (!stateMachine.animator.IsInTransition(0) 
            && ( anim.IsTag("Attack") || anim.IsTag("TakeDown") )
            && currentQuickTarget != null)
        {
            if (anim.normalizedTime > 1)
            currentQuickTarget = null;            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if (target == null) return;

        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }

        RemoveTarget(target);
        canTakeDown = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Enemy")
        {
            TakeDownCheck(other);
        }
    }
    public void SelectClosestTarget()
    {
        if (currentQuickTarget != null) { return; }
        if (targets.Count == 0) { return; }
        float closestDistance = Mathf.Infinity;
        Target closestTarget = null;

        foreach (Target target in targets)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, target.transform.position);

            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closestTarget = target;
                //Debug.Log(closestTarget + " loop");

            }
        }
        currentQuickTarget = closestTarget;
        
        //Debug.Log(closestTarget.transform.name);
        Vector3 lookPos = closestTarget.transform.position - stateMachine.transform.position;
        lookPos.y = 0f;

        if (Vector3.Distance(closestTarget.transform.position, stateMachine.transform.position) < 4f)
            stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);

        if (closestTarget.tag == "Enemy")
        {
            if (closestTarget.TryGetComponent<Health>(out Health health))
            {
                Debug.Log("Found Enemy Health Component");
                if (health.isStunned)
                    canTakeDown = true;
                else
                    canTakeDown = false;
            }
        }
    }
    
    public void CycleTarget()
    {
        if (targets.Count == 0) { return; }
        if (currentTarget == null) { return; }

        Debug.Log("Target Count: " + targets.Count);

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].name == currentTarget.name)
            {
                index = i;
                break;
            }
        }


        if (stateMachine.InputReader.SelectionValue.x >= .9f)
        {
            if (index == targets.Count - 1 && didCycle == false)
            {
                Debug.Log("Running this block index >= targets.count");
                didCycle = true;
                index = 0;
                currentTarget = targets[index];

            }

            if (index < targets.Count && didCycle == false)
            {
                didCycle = true;
                index += 1;
                currentTarget = targets[index];
            }
        }

        if (stateMachine.InputReader.SelectionValue.x <= -.9f)
        {
            if (index == 0 && didCycle == false)
            {
                Debug.Log("Running this block index >= targets.count");
                didCycle = true;
                index = targets.Count - 1;
                currentTarget = targets[index];

            }

            if (index < targets.Count && didCycle == false)
            {
                didCycle = true;
                index -= 1;
                currentTarget = targets[index];
            }
        }

        if (stateMachine.InputReader.SelectionValue.x == 0 && didCycle)
            didCycle = false;

    }

    public bool SelectTarget()
    {

        if (targets.Count == 0) { return false; }

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity;

        foreach (Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            if (!target.GetComponentInChildren<Renderer>().isVisible)
            {
                continue;
            }

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f);
            if (toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null) { return false; }


        currentTarget = closestTarget;
        cineTargetGroup.AddMember(currentTarget.transform, 1f, 2f);

        return true;
    }

    public void Cancel()
    {
        if (currentTarget == null) { return; }
        cineTargetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if (currentTarget == target)
        {
            cineTargetGroup.RemoveMember(currentTarget.transform);
            currentTarget = null;
        }
        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }

    
    public void TakeDownCheck(Collider other)
    {
        Debug.Log("Checking");
        other.TryGetComponent(out Health enemy);
        if (enemy.isStunned)
            canTakeDown = true;
        else
            canTakeDown = false;
    }
    
    
    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, collider.radius);
    //}


}
