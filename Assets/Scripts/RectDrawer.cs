using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectDrawer
{
    InputController inputController;
    float selectionBoxAccuracy;
    bool isSelecting = false;
    Vector3 mousePosition1;
    Camera camera;

    //void Start()
    //{
    //    inputController = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
    //}

    //void Update()
    //{
    //    MouseClickControl();
    //}

    public void SetCamera(Camera camera)
    {
        this.camera = camera;
    }

    public void SetInputController(InputController inputController)
    {
        this.inputController = inputController;
    }

    public void SetSelectionBoxAccuracy(float selectionBoxAccuracy)
    {
        this.selectionBoxAccuracy = selectionBoxAccuracy;
        Debug.Log(this.selectionBoxAccuracy);
    }

    public void MouseClickControl()
    {
        // If we press the left mouse button, save mouse location and begin selection
        if (inputController.leftMouseButtonDown)
        {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;
        }
        // If we let go of the left mouse button, end selection
        if (inputController.leftMouseButtonUp)
            isSelecting = false;
    }

    //void OnGUI()
    //{
    //    DrawRect();
    //}

    public void DrawRect()
    {
        if (isSelecting &&
            Vector3.Distance(camera.ScreenToViewportPoint(mousePosition1),
            camera.ScreenToViewportPoint(Input.mousePosition)) > selectionBoxAccuracy)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
            // Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
}
