using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform mainCameraTransform;
    public float panSpeed = 20f;
    public float verticalSpeed = 10f;

    public float minVerticalRotation = 0f;
    public float maxVerticalRotation = 65f;
    public float minVerticalPosition = 2f;
    public float maxVerticalPosition = 15f;

    private bool verticalRotationEnabled = true;
    private InputController inputController;

    private void Start()
    {
        inputController = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputController>();
    }

    void LateUpdate()
    {
        PlanePositionUpdate();
        RotationUpdate();
        VerticalPositionUpdate();
    }

    private void PlanePositionUpdate()
    {
        Vector3 pos = transform.position;

        Vector3 moveVector = Vector3.zero;
        if (inputController.forwardMove)
        {
            moveVector += transform.forward.normalized;
        }

        if (inputController.backwardMove)
        {
            moveVector -= transform.forward.normalized;
        }

        if (inputController.leftMove)
        {
            moveVector -= transform.right.normalized;
        }

        if (inputController.rightMove)
        {
            moveVector += transform.right.normalized;
        }

        moveVector.y = 0;
        moveVector = moveVector.normalized * panSpeed * Time.deltaTime;
        transform.position += moveVector;
    }

    private void RotationUpdate()
    {
        var easeFactor = 10f;
        if (inputController.midMouseButtonPressed)
        {
            // Horizontal
            if (Mathf.Abs(inputController.deltaXMouseMove) > 0)
            {
                var cameraRotationY = inputController.deltaXMouseMove * easeFactor * Time.deltaTime;
                transform.Rotate(0, cameraRotationY, 0);
            }

            // Vertical
            if (Mathf.Abs(inputController.deltaYMouseMove) > 0)
            {
                var cameraRotationX = inputController.deltaYMouseMove * easeFactor * Time.deltaTime;
                var desiredRotation = Mathf.Clamp(cameraRotationX + mainCameraTransform.rotation.eulerAngles.x, minVerticalRotation, maxVerticalRotation);
                mainCameraTransform.rotation = Quaternion.Euler(desiredRotation, mainCameraTransform.rotation.eulerAngles.y, mainCameraTransform.rotation.eulerAngles.z);
            }
        }
    }

    private void VerticalPositionUpdate()
    {
        // Move down
        if (inputController.mouseWheelUp)
        {
            var desiredCameraPositionY = Mathf.Clamp(transform.position.y - verticalSpeed * Time.deltaTime, minVerticalPosition, maxVerticalPosition);
            transform.position = new Vector3(transform.position.x, desiredCameraPositionY, transform.position.z);
        }
        // Move up
        if (inputController.mouseWheelDown)
        {
            var desiredCameraPositionY = Mathf.Clamp(transform.position.y + verticalSpeed * Time.deltaTime, minVerticalPosition, maxVerticalPosition);
            transform.position = new Vector3(transform.position.x, desiredCameraPositionY, transform.position.z);
        }
    }
}
