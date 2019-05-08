using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float panBorderThickness = 10f;
    public bool takeBordersIntoAccount;
    public bool forwardMove;
    public bool backwardMove;
    public bool rightMove;
    public bool leftMove;
    public bool leftShiftButtonPressed;
    public bool leftMouseButtonDown;
    public bool leftMouseButtonUp;
    public bool leftMouseButtonPressed;
    public bool rightMouseButtonPressed;
    public bool midMouseButtonPressed;
    public float deltaXMouseMove;
    public float deltaYMouseMove;
    public bool mouseWheelUp;
    public bool mouseWheelDown;

    private float mouseX = 0;
    private float mouseY = 0;
    void Update()
    {
        // Move buttons
        if (takeBordersIntoAccount)
        {
            forwardMove = Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness;
            backwardMove = Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness;
            leftMove = Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness;
            rightMove = Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness;
        }
        else
        {
            forwardMove = Input.GetKey(KeyCode.W);
            backwardMove = Input.GetKey(KeyCode.S);
            leftMove = Input.GetKey(KeyCode.A);
            rightMove = Input.GetKey(KeyCode.D);
        }
        leftShiftButtonPressed = Input.GetKey(KeyCode.LeftShift);
        ////////////////

        // Mouse buttons
        leftMouseButtonDown = Input.GetMouseButtonDown(0);
        leftMouseButtonUp = Input.GetMouseButtonUp(0);
        midMouseButtonPressed = Input.GetMouseButton(2);
        leftMouseButtonPressed = Input.GetMouseButton(0);
        rightMouseButtonPressed = Input.GetMouseButton(1);
        ////////////////
        
        // Scroll wheel
        mouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        mouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
        ////////////////

        // Mouse move
        deltaXMouseMove = Input.mousePosition.x - mouseX;
        deltaYMouseMove = mouseY - Input.mousePosition.y;
        mouseX = Input.mousePosition.x;
        mouseY = Input.mousePosition.y;
        ////////////////
    }
}
