using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    public Vector2 CameraValue { get;private set; }

    private Controls controls;

    public bool JumpButtonPressed => controls.Player.Jump.WasPressedThisFrame();


    //camera    
    Transform cam;

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("Camera Rotation Speed")]
    public float CameraRotationSpeed = 3f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    private void Start()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
        Debug.Log(MovementValue);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnModifier(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }

    public void OnCamera(InputAction.CallbackContext context)
    {
        CameraValue = context.ReadValue<Vector2>();
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void CameraRotation()
    {
        if (CameraValue.sqrMagnitude >= _threshold)
        {
            float deltaTimeMultiplier = Time.deltaTime;

            _cinemachineTargetYaw -= CameraValue.x;
            _cinemachineTargetPitch -= CameraValue.y;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);

    }
}
