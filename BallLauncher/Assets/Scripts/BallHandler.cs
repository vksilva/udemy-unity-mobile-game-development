using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private Rigidbody2D currentBallRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Touchscreen.current.primaryTouch.press.isPressed)
        {
            currentBallRigidbody.isKinematic = false;
            return;
        }

        currentBallRigidbody.isKinematic = true;
        
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldTouchPosition2 = mainCamera.ScreenToWorldPoint(touchPosition);
        
        Debug.Log(touchPosition);
        Debug.Log(worldTouchPosition2);

        currentBallRigidbody.position = worldTouchPosition2;
    }
}
