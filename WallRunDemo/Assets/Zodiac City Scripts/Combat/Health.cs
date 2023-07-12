using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float maxEnergy = 100;
    [SerializeField] private int health;
    [SerializeField] private float energy;
    [SerializeField, Range(0f, 100f)] public int stunMeter = 0;
    [SerializeField, Range(0, 6f)] private float stunWaitTime;
    [SerializeField] public bool isStunned;
    [SerializeField] private float maxStunWaitTime = 4f;
    [field: SerializeField] public Coroutine stunDecay;
    [SerializeField] Animator animator;
    public bool isInvunerable;
    public bool isDead => health <= 0;

    public event Action OnTakeDamage;
    public event Action OnDie;
    //public event Action OnStunned;
    public string type = "";


    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
        energy = maxEnergy;
        stunWaitTime = maxStunWaitTime;
        //animator.GetComponent<Animator>();

    }

    public void Update()
    {
        //check to see if stun meter is greater then 0f;
        //if so decrease stun wait time.
        //if Stun wait time = 0; decrease stunmeter;
        //if stunmeter = 0 reset stun wait time;
        
        if(this.gameObject.tag == "Enemy")
        {
            if (stunMeter > 0f && stunMeter < 100f)
            {
                stunWaitTime -= Time.deltaTime;
                stunWaitTime = Mathf.Clamp(stunWaitTime, 0f, maxStunWaitTime);
            }

            if (stunMeter >= 100 && isStunned == false)
            {
                isStunned = true;
            }           

            if (stunWaitTime <= 0)
            {
                if (stunDecay == null)
                {
                    Debug.Log(stunDecay);
                    stunDecay = StartCoroutine(StunDecay());
                }
            }
            else
            {
                StopCoroutine(StunDecay());
                stunDecay = null;
            }
        }
        
    }


    public void DealDamage(int damage, int stun)
    {
        if (health == 0) return;
        if (isInvunerable) return;
        //if(this.gameObject.tag == "Enemy")
        //{
        //    if (isStunned)
        //    {
        //        Debug.Log("invoking stun method");
        //        Debug.Log("Stunned through Combat");
        //        OnStunned?.Invoke();
        //    }           
        //}
      

        health = Mathf.Max(health - damage, 0);
        Debug.Log($"Stun Meter: {stunMeter} \n Stun: {stun}");
        stunMeter += stun;
        Debug.Log($"Stun Meter: {stunMeter} \n Stun: {stun}");

       
        
        OnTakeDamage?.Invoke();
        //Debug.Log(health);
        //Debug.Log("health script: " + type);        
        if (isDead && !animator.GetCurrentAnimatorStateInfo(0).IsName("KnockDown"))
        {
            OnDie?.Invoke();
        }

    }

    private IEnumerator StunDecay()
    {
        while (stunMeter > 0)
        {
            stunMeter -= 1;
            yield return new WaitForSeconds(0.2f);
        }
        stunWaitTime = maxStunWaitTime;
    }


    public void SetEnergy(int value)
    {
        energy = MathF.Max(energy + value, 0);
    }

    public void SetInvunerable(bool isInvunerable)
    {
        this.isInvunerable = isInvunerable;
    }


}
