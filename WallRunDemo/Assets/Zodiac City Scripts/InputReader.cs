using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public bool IsAttacking { get; private set; }
    public bool IsStrongAttacking { get; private set; }
    public bool IsBlocking { get; private set; }
    [field: SerializeField] public bool ReadySkill { get; private set; }

    [SerializeField] private float lastTimePressed;
    [SerializeField] private float delay;
    public Vector2 MovementValue { get; private set; }
    public Vector2 CameraValue; 


    public Vector2 SelectionValue { get; private set; }

    public event Action JumpEvent;

    public event Action AttackEvent;

    public event Action StrongAttackEvent;

    public event Action DodgeEvent;

    public event Action TargetEvent;

    public event Action CancelEvent;

    public event Action TakeDownEvent;



    public bool targeting = false;

    public bool modified = false;

    public bool TakeDownInitiated = false;

    private Controls controls;


    public bool TakeDownButtonPressed => controls.Player.TakeDown.WasPressedThisFrame();
    public bool AttackButtonPressed => controls.Player.Attack.WasPressedThisFrame();
    public bool StrongAtkButtonPressed => controls.Player.StrongAttack.WasPressedThisFrame();
    public bool JumpButtonPressed => controls.Player.Jump.WasPressedThisFrame();
    public bool DodgeButtonPressed => controls.Player.Dodge.WasPressedThisFrame();

    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;

    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;

    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;

    [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;

    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;

    private GameObject _mainCamera;

    private void Awake()
    {
        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }


    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void LateUpdate()
    {
        Debug.Log("InputReader:: Update Function");
        CameraRotation();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnTakeDown(InputAction.CallbackContext context)
    {


        if (context.started)
        {
            TakeDownInitiated = true;
            Debug.Log("Take Down " + TakeDownInitiated);
            return;
        }

        if (context.canceled)
        {
            TakeDownInitiated = false;
            Debug.Log("Take Down " + TakeDownInitiated);
        }
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; };
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; };
        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
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
        if(CameraValue.sqrMagnitude >= _threshold)
        {
            float deltaTimeMultiplier = Time.deltaTime;

            _cinemachineTargetYaw -= CameraValue.x ;
            _cinemachineTargetPitch -= CameraValue.y ;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);

    }


    public void OnTargeting(InputAction.CallbackContext context)
    {
        //if (!context.performed) { return; }
        if (context.performed && !targeting)
        {
            targeting = true;
            TargetEvent?.Invoke();
        }
        else if (context.performed && targeting)
        {
            targeting = false;
            CancelEvent?.Invoke();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        AttackEvent?.Invoke();
        return;
    }

    public void OnStrongAttack(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        StrongAttackEvent?.Invoke();
        return;
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsBlocking = true;
        }
        else if (context.canceled)
        {
            IsBlocking = false;
        }
    }

    public void OnModifier(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            modified = true;
        }
        else if (context.canceled)
        {
            modified = false;
        }

    }

    public void OnTargetSelection(InputAction.CallbackContext context)
    {
        SelectionValue = context.ReadValue<Vector2>();
        //Debug.Log(SelectionValue.x);
    }

    public void OnSkills(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ReadySkill = true;
        }
        else if (context.canceled)
        {
            ReadySkill = false;
        }

    }


}
