using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsLookAtCamera : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Leave empty to look at main camera")]
    private GameObject Target = null;

    // private Vector3 TargetPosition;
    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (Target == null)
        {
            Target = gameManager.mainCamera;
        }
    }

    private void LateUpdate()
    {
        transform.LookAt(Target.transform.position);
    }
}
