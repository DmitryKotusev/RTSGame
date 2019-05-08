using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsSelector : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickablesLayer;
    List<GameObject> selectedObjects;
    InputController inputController;

    [HideInInspector]
    public List<GameObject> selectableObjects;

    Vector3 pos1;
    Vector3 pos2;
    void Awake()
    {
        selectedObjects = new List<GameObject>();
        selectableObjects = new List<GameObject>();
        inputController = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputController.leftMouseButtonDown)
        {
            pos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            RaycastHit raycastHit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, Mathf.Infinity, clickablesLayer)) {
                AgentOnSelect agentOnSelectScript = raycastHit.transform.GetComponent<AgentOnSelect>();
                if (inputController.leftShiftButtonPressed)
                {
                    selectedObjects.Add(raycastHit.transform.gameObject);
                    agentOnSelectScript.OnSelect();
                }
                else
                {
                    ClearSelection();
                    selectedObjects.Add(raycastHit.transform.gameObject);
                    agentOnSelectScript.OnSelect();
                }
            }
            else
            {
                ClearSelection();
            }
        }

        if (inputController.leftMouseButtonUp)
        {
            pos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (pos1 != pos2)
            {
                SelectObjects();
            }
        }
    }

    private void SelectObjects()
    {
        List<GameObject> remObjects = new List<GameObject>();

        if (!inputController.leftShiftButtonPressed)
        {
            ClearSelection();
        }

        Rect selectRect = new Rect(pos1.x, pos1.y, pos2.x - pos1.x, pos2.y - pos1.y);
        foreach(GameObject obj in selectableObjects)
        {
            if (obj != null)
            {
                if (selectRect.Contains(Camera.main.WorldToViewportPoint(obj.transform.position), true))
                {
                    if (!selectedObjects.Contains(obj))
                    {
                        selectedObjects.Add(obj);
                        obj.GetComponent<AgentOnSelect>().OnSelect();
                    }
                }
            }
            else
            {
                remObjects.Add(obj);
            }
        }
        foreach (GameObject obj in remObjects)
        {
            selectableObjects.Remove(obj);
        }
        remObjects.Clear();
    }

    private void ClearSelection()
    {
        foreach (GameObject obj in selectedObjects)
        {
            obj.GetComponent<AgentOnSelect>().OnSelect();
        }

        selectedObjects.Clear();
    }
}
