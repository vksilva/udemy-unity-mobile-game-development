using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public static Action<Asteroid> returnToPool;
    
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
}
