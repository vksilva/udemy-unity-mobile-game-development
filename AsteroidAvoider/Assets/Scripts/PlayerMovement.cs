using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private Rigidbody playerRigidbody;

    private Vector3 movementDirection;
    private Camera mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;

        Vector3 viewportPosition = mainCamera.WorldToViewportPoint(newPosition);
        
        if (viewportPosition.x > 1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }

        if (viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }

        if (viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }

        if (viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        transform.position = newPosition;
        
    }

    private void FixedUpdate()
    {
        if(movementDirection == Vector3.zero){return;}
        
        playerRigidbody.AddForce(movementDirection * forceMagnitude, ForceMode.Force);

        playerRigidbody.velocity = Vector3.ClampMagnitude(playerRigidbody.velocity, maxVelocity);
    }

    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            //Detect input position
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            //Calculate movement to input position
            movementDirection = (worldPosition - transform.position);
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }
}
