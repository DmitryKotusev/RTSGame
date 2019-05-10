using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform launchTransform;
    public GameObject bullet;
    public WeaponConfig weaponConfig;

    float latestShotTime;

    private void Start()
    {
        latestShotTime = 0;
    }

    public bool TryFire()
    {
        if (Time.time - latestShotTime > weaponConfig.rateOfFire)
        {
            Fire();
        }
        return false;
    }

    public void Fire()
    {
        latestShotTime = Time.time;
        GameObject bulletClone = Instantiate(bullet, launchTransform.position, launchTransform.rotation);
        // SetBulletsParameters
        Bullet bulletScript = bulletClone.GetComponent<Bullet>();
        bulletScript.SetLifeTime(weaponConfig.bulletLifeTime);
        bulletClone.GetComponent<Rigidbody>().AddForce(launchTransform.transform.up * weaponConfig.bulletSpeed);
    }
}
