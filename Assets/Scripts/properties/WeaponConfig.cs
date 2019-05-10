using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Config")]
public class WeaponConfig : ScriptableObject
{
    public float damage;
    public float rateOfFire;
    public float bulletSpeed;
    public float bulletLifeTime;
    public WeaponType weaponType;
}
