using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    public event Action<Vector3, Vector3> onLedgeDetect;
    private void OnTriggerEnter(Collider other)
    {
        onLedgeDetect?.Invoke(other.ClosestPointOnBounds(transform.position), other.transform.forward);
    }
}
