using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameObject> selectableObjects;
    public GameObject mainCamera;

    public void Awake()
    {
        selectableObjects = new List<GameObject>();
    }

    public List<GameObject> GetSelectableObjects()
    {
        return selectableObjects;
    }
}
