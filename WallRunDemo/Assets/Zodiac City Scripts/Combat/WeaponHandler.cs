using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogicL;
    [SerializeField] private GameObject weaponLogicR;
    [SerializeField] private GameObject weaponLogicLL;
    [SerializeField] private GameObject weaponLogicRL;

    private void EnableWeapon(string collider)
    {
        switch (collider)
        {
            case "L":
                this.weaponLogicL?.SetActive(true);
                break;
            case "R":
                this.weaponLogicR?.SetActive(true);
                break;
            case "LL":
                this.weaponLogicLL?.SetActive(true);
                break;
            case "RL":
                this.weaponLogicRL?.SetActive(true);
                break;
        }
    }

    private void DisableWeapon()
    {
        //Debug.Log(gameObject.name + "hit box off");
        this.weaponLogicR?.SetActive(false);
        this.weaponLogicL?.SetActive(false);
        this.weaponLogicLL?.SetActive(false);
        this.weaponLogicRL?.SetActive(false);
    }
}
