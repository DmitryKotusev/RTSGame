using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentData : MonoBehaviour
{
    public GameObject weapon;
    public AgentTeam agentTeam;
    public float lookDistance;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public float GetPriorityСoefficient(float distance)
    {
        float result = 0;
        WeaponScript weaponScript = weapon.GetComponent<WeaponScript>();
        result += weaponScript.weaponConfig.damage * gameManager.damageCoefficient
            + weaponScript.weaponConfig.rateOfFire * gameManager.rateOfFireCoefficient
            + weaponScript.weaponConfig.bulletSpeed * gameManager.bulletSpeedCoefficient;
        Health healthScript = GetComponent<Health>();
        result += 1 / healthScript.GetHealthPoints() * gameManager.healthCoefficient;
        result += 1 / distance * gameManager.distanceCoefficient;
        return result;
    }
}
