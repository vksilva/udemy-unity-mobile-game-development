using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float speedGainPerSecond = 0.2f;
    [SerializeField] private float turnSpeed = 200f;

    private int steerValue;
    
    void Update()
    {
        speed += speedGainPerSecond * Time.deltaTime;
        
        transform.Rotate(0f, steerValue * turnSpeed * Time.deltaTime, 0f);
        
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pew");
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene("Scene_MainMenu");
        }
    }

    public void Steer(int value)
    {
        steerValue = value;
    }
}
