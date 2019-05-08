using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSelect : MonoBehaviour
{
    InputController inputController;
    bool isSelecting = false;
    Vector3 mousePosition1;

    void Start()
    {
        inputController = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
    }

    void Update()
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

    void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
            // Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }
}
