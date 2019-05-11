using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentData : MonoBehaviour
{
    public GameObject weapon;
    public AgentTeam agentTeam;
    public float lookDistance;

    public float GetPriorityСoefficient(float distance)
    {
        float result = 0;
        WeaponScript weaponScript = weapon.GetComponent<WeaponScript>();
        result += weaponScript.weaponConfig.damage
            + weaponScript.weaponConfig.rateOfFire
            + weaponScript.weaponConfig.bulletSpeed * 0.2f;
        Health healthScript = GetComponent<Health>();
        result += 1 / healthScript.GetHealthPoints();
        result += 1 / distance;
        return result;
    }
}
