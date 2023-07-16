using System.Collections;
using System.Collections.Generic;
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

    [field:Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    public Transform orientation;
    public CharacterController characterController;

    public void Update()
    {
        CheckForWall();
    }

    public void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, whatIsWall);
        Debug.DrawRay(transform.position, orientation.right * wallCheckDistance, Color.blue);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, whatIsWall);
        Debug.DrawRay(transform.position, -orientation.right * wallCheckDistance, Color.red);
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


}
