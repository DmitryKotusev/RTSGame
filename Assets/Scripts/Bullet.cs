using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage;
    float timeOfBirth;
    float timeToLive;
    bool isBorn;

    private void Start()
    {
    }

    void Update()
    {
        if (isBorn)
        {
            CheckLifeSpan();
        }
    }

    public void SetLifeTime(float timeToLive)
    {
        isBorn = true;
        timeOfBirth = Time.time;
        this.timeToLive = timeToLive;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    void CheckLifeSpan()
    {
        if (Time.time - timeOfBirth > timeToLive)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Agent")
        {
            Health targetHealth = col.transform.GetComponent<Health>();
            targetHealth.TakeDamage(damage);
        }
        Debug.Log("Collision detected: " + col.transform.name);
        Destroy(gameObject);
    }
}
