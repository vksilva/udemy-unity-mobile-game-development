using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private float detachDelay;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Rigidbody2D pivot;
    [SerializeField] private float respawDelay;
    
    private Rigidbody2D currentBallRigidbody;
    private SpringJoint2D currentBallSpringJoint;

    private Camera mainCamera;
    private bool isDragging;
    
    void Start()
    {
        mainCamera = Camera.main;
        SpawNewBall();
    }

    void Update()
    {
        if (currentBallRigidbody == null)
        {
            return;
        }
        
        if(!Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isDragging)
            {
                LaunchBall();
            }

            isDragging = false;
            return;
        }

        isDragging = true;
        currentBallRigidbody.isKinematic = true;
        
        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 worldPosition2 = mainCamera.ScreenToWorldPoint(touchPosition);

        currentBallRigidbody.position = worldPosition2;
    }

    private void SpawNewBall()
    {
        GameObject ballInstance = Instantiate(ballPrefab, pivot.position, Quaternion.identity);
        currentBallRigidbody = ballInstance.GetComponent<Rigidbody2D>();
        currentBallSpringJoint = ballInstance.GetComponent<SpringJoint2D>();
        currentBallSpringJoint.connectedBody = pivot;

    }

    private void LaunchBall()
    {
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;
        
        Invoke(nameof(DetachBall), detachDelay);
        Invoke(nameof(SpawNewBall), respawDelay+detachDelay);
    }

    private void DetachBall()
    {
        currentBallSpringJoint.enabled = false;
        currentBallSpringJoint = null;
    }
}
