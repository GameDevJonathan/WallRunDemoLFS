using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skills
{
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public string AnimationTag { get; private set; }
    [field: SerializeField] public float TransitionDuration { get; private set; }    
    [field: SerializeField] public float ForceTime { get; private set; }
    [field: SerializeField] public float Force { get; private set; }
    [field: SerializeField] public float UpForce { get; private set; } = 0f;
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float KnockBack { get; private set; }
    [field: SerializeField] public string AttackType { get; private set; }
}
