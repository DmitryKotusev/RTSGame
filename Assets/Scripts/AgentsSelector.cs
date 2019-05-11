using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsSelector : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickablesLayer;
    [SerializeField]
    private float selectionBoxAccuracy;
    List<GameObject> selectedObjects;
    InputController inputController;

    Vector3 pos1;
    Vector3 pos2;

    RectDrawer rectDrawer;
    Camera mainCamera;
    GameManager gameManager;

    public List<GameObject> GetSelectedObjects()
    {
        return selectedObjects;
    }

    void Awake()
    {
        selectedObjects = new List<GameObject>();
        inputController = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        mainCamera = GetComponentInChildren<Camera>();

        rectDrawer = new RectDrawer();
        rectDrawer.SetInputController(inputController);
        rectDrawer.SetSelectionBoxAccuracy(selectionBoxAccuracy);
        rectDrawer.SetCamera(mainCamera);
    }

    // Update is called once per frame
    void Update()
    {
        rectDrawer.MouseClickControl();
        SelectWithRect();
        SelectWithSingleClick();
    }

    private void OnGUI()
    {
        rectDrawer.DrawRect();
    }

    private void SelectWithRect()
    {
        if (inputController.leftMouseButtonDown)
        {
            pos1 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        if (inputController.leftMouseButtonUp)
        {
            pos2 = mainCamera.ScreenToViewportPoint(Input.mousePosition);
            if (Vector3.Distance(pos1, pos2) > selectionBoxAccuracy)
            {
                SelectObjects();
            }
        }
    }

    private void SelectWithSingleClick()
    {
        if (Vector3.Distance(pos1, pos2) <= selectionBoxAccuracy)
        {
            if (inputController.leftMouseButtonUp)
            {
                RaycastHit raycastHit;

                if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out raycastHit, Mathf.Infinity, clickablesLayer))
                {
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
        foreach(GameObject obj in gameManager.GetSelectableObjects())
        {
            if (obj != null)
            {
                if (selectRect.Contains(mainCamera.WorldToViewportPoint(obj.transform.position), true))
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
            gameManager.GetSelectableObjects().Remove(obj);
        }
        remObjects.Clear();
    }

    private void ClearSelection()
    {
        foreach (GameObject obj in selectedObjects)
        {
            if (obj != null)
            {
                obj.GetComponent<AgentOnSelect>().OnSelect();
            }
        }

        selectedObjects.Clear();
    }
}
