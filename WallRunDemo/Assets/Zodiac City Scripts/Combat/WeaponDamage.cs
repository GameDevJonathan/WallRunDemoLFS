using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider myCollider;
    [SerializeField] private List<Collider> collidedWith = new List<Collider>();    
    private int damage;
    private float knockBack;
    string type;
    int stunDamage;



    private void OnEnable()
    {
        collidedWith.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;

        if (collidedWith.Contains(other))return;

        collidedWith.Add(other);


        if(other.TryGetComponent<Health>(out Health health))
        {

            if (health.isInvunerable) return;
            health.type = type;            
            health.DealDamage(damage,stunDamage);

            if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
            {
                Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
                forceReceiver.AddForce(direction * knockBack);
            }

        }

        //if(other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        //{
        //    Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
        //    forceReceiver.AddForce(direction * knockBack);
        //}

    }

    public void SetAttack(int damage, float knockBack, string type, int sDamage = 10)
    {
        this.damage = damage;
        this.knockBack = knockBack;
        this.type = type;
        this.stunDamage = sDamage;

    }
}
