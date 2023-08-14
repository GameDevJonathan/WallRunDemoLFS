using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    public ParticleSystem[] Slash;

    public void UpdateSlash(int index)
    {
        Slash[index].Play();        
    }

}
