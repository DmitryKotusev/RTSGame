using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // float speed;
    float damage;
    float timeOfBirth;
    float timeToLive;
    bool isBorn;

    private void Start()
    {
    }

    void Update()
    {
        // Move();
        if (isBorn)
        {
            CheckLifeSpan();
        }
    }

    //public void SetSpeed(float speed)
    //{
    //    this.speed = speed;
    //}

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

    //void Move()
    //{
    //    transform.Translate(Vector3.up * speed * Time.deltaTime);
    //}

    void OnCollisionEnter(Collision col)
    {
        // Explosion
        // GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
        // Destroy(e, 1.5f);
        if (col.transform.tag == "Agent")
        {
            Health targetHealth = col.transform.GetComponent<Health>();
            targetHealth.TakeDamage(damage);
        }
        Debug.Log("Collision detected: " + col.transform.name);
        Destroy(gameObject);
    }
}
