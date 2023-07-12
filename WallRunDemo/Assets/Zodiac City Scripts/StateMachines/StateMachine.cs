using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State currentState;
           
    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void StartRoutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }

    public void Create(GameObject prefab, GameObject spawn_point)
    {
        Instantiate(prefab, spawn_point.transform.position,spawn_point.transform.rotation);
    }
}
