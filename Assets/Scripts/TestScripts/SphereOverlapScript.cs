using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereOverlapScript : MonoBehaviour
{
    [SerializeField]
    LayerMask agentsMask;
    [SerializeField]
    float radius;
    void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, agentsMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Debug.Log(hitColliders[i].transform.name);
            i++;
        }
    }
}
