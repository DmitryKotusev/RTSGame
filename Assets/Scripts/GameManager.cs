using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> selectableObjects;
    public GameObject mainCamera;

    public float damageCoefficient = 4;
    public float rateOfFireCoefficient = 5;
    public float bulletSpeedCoefficient = 0.01f;
    public float healthCoefficient = 3;
    public float distanceCoefficient = 1;

    public void Awake()
    {
        selectableObjects = new List<GameObject>();
    }

    public List<GameObject> GetSelectableObjects()
    {
        return selectableObjects;
    }
}
