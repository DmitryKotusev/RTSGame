using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public bool canRegenerate;
    public float regenerateSpeed = 2;
    [Tooltip("Once this time has passed the health increases by regenerateSpeed")]
    public float increaseHealthTime = 10;
    public float maxPoints = 100;
    public GameObject deathParticles;

    [Header("HealthBar reference")]
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    float healthPoints;
    float currentTime;

    private void Start()
    {
        currentTime = Time.time;
        healthPoints = maxPoints;
    }

    private void Update()
    {
        Regenerate();
    }

    public void SetHealthPoints(float healthPoints)
    {
        this.healthPoints = healthPoints;
    }

    public float GetHealthPoints()
    {
        return healthPoints;
    }

    public void TakeDamage(float damagePoints)
    {
        healthPoints = Mathf.Clamp(healthPoints - damagePoints, 0, maxPoints);
        if (healthPoints == 0)
        {
            GameObject particles = Instantiate(deathParticles, transform.position, transform.rotation);
            Destroy(particles, 2f);
            Destroy(gameObject);
        }
    }

    public void TreatHealth(float treatPoints)
    {
        healthPoints = Mathf.Clamp(healthPoints + treatPoints, 0, maxPoints);
    }

    public void Regenerate()
    {
        if (canRegenerate)
        {
            if (Mathf.Abs(Time.time - currentTime) > increaseHealthTime && healthPoints < maxPoints)
            {
                currentTime = Time.time;
                healthPoints = Mathf.Clamp(healthPoints + regenerateSpeed, 0, maxPoints);
            }
        }
    }

    public void SynchronizeHealthOnBar()
    {
        healthBar.fillAmount = healthPoints / maxPoints;
    }
}
