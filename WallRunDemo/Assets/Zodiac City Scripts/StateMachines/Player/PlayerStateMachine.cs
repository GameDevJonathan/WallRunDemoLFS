using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStateMachine : StateMachine
{
    public Transform MainCameraTransform { get; private set; }
    [field:Header("Required Components")]
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController characterController { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }
    [field: SerializeField] public Targeter targeter { get; private set; }
    [field: SerializeField] public Transform CameraFocus { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReciever { get; private set; }
    [field: SerializeField] public WeaponDamage weaponL { get; private set; }
    [field: SerializeField] public WeaponDamage weaponR { get; private set; }
    [field: SerializeField] public WeaponDamage weaponLL { get; private set; }
    [field: SerializeField] public WeaponDamage weaponRL { get; private set; }
    [field: SerializeField] public GameObject DragonWaveSpawnPoint { get; private set; }
    [field: SerializeField] public GameObject DragonWave { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    [field: SerializeField] public EnviormentScaner EnviormentScaner { get; private set; }
    
    [field:Header("Movement Values")]
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float RotationSmoothValue { get; private set; }
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeLength { get; private set; }
    [field: SerializeField] public float AirSpeed { get; private set; }

    [field: SerializeField] public float JumpForce { get; private set; }
    
    [field:Space(10)]

    [field:Header("Player Actions and Attacks")]
    [field: SerializeField] public List<ParkourAction> ParkourActions { get; private set; }

    [field: SerializeField] public Attack[] Attacks { get; private set; }
    [field: SerializeField] public Skills[] Skills { get; private set; }

    


    // Start is called before the first frame update
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        MainCameraTransform = Camera.main.transform;

        SwitchState(new PlayerFreeLookState(this));
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
        SwitchState(new PlayerImpactState(this));
    }

    private void HandleDeath()
    {
        SwitchState(new PlayerDeadState(this));
    }

    public void DragonWaveSkill()
    {
        //Create(DragonWave, DragonWaveSpawnPoint);
        Instantiate(DragonWave, DragonWaveSpawnPoint.transform.position, 
            DragonWaveSpawnPoint.transform.rotation);
    }   
}
