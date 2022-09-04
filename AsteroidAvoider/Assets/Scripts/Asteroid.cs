using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    public static Action<Asteroid> returnToPool;
    public Vector3 rotationDirection;
    public float rotationSpeed;

    public void Initialize()
    {
        rotationSpeed = Random.Range(5, 100);
        rotationDirection = Random.insideUnitSphere;
        if (rotationDirection == Vector3.zero)
        {
            rotationDirection = Vector3.up;
        }
        
        rotationDirection.Normalize();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        
        if(playerHealth == null){return;}
        
        playerHealth.Crash();
    }

    private void OnBecameInvisible()
    {
        returnToPool?.Invoke(this);
    }

    private void Update()
    {
        transform.Rotate(rotationDirection, rotationSpeed * Time.deltaTime);
    }
}
