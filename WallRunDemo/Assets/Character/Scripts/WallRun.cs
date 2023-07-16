using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("WallRunning")]
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;

    public float wallRunForce;
    public float wallRunTime;
    public float maxWallRunTime;
    public float wallRunTimer;

    [Header("Input")]
    public InputReader inputReader;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    [SerializeField] public RaycastHit leftWallHit;
    [SerializeField] public RaycastHit rightWallHit;
    public bool wallLeft;
    public bool wallRight;

    [Header("References")]
    public Transform orientation;
    public CharacterController characterController;
    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        CheckForWall();
        
        
    }

    public void CheckForWall()
    {
        
        wallRight = Physics.Raycast(orientation.position, orientation.right, out rightWallHit,wallCheckDistance, whatIsWall);
        Debug.DrawRay(orientation.position, orientation.right * wallCheckDistance, Color.blue);
        
        
        wallLeft = Physics.Raycast(orientation.position, -orientation.right, out leftWallHit,wallCheckDistance, whatIsWall);
        Debug.DrawRay(orientation.position, -orientation.right * wallCheckDistance, Color.red);
        
        
        Debug.DrawRay(transform.position, Vector3.down * minJumpHeight, Color.magenta);
    }

    public bool AboveGround()
    {

        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    public bool HitWall()
    {
        if (wallLeft || wallRight)
            return true;
        else
            return false;
    }

    public void WallRunningMovement()
    {        
        
        Vector3 walllNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(walllNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;        
        
        characterController.Move(wallForward * wallRunForce * Time.deltaTime);
        //push to wall
        
        if(!(wallLeft && inputReader.MovementValue.x > 0) && !(wallRight && inputReader.MovementValue.x < 0))
        {
            characterController.Move(-walllNormal * 100 * Time.deltaTime);
        }
    }


}
