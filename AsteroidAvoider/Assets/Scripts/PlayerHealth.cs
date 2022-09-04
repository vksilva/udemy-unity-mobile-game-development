using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int spaceShipMaxHealth;
    [SerializeField] private GameOverHandler gameOverHandler;
    [SerializeField] private float invulnerableTime;
    [SerializeField] private float adInvulnerableTime;
    [SerializeField] private float blinkDurationTime;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material invulnerableMaterial;
    [SerializeField] private Material shipMaterial;
    
    private float lastHitTime;
    private float lastBlinkAnimationTime;
    private int spaceShipHealth;
    private bool backFromInvulnerable;
    private bool blinkActive = false;

    private bool isVulnerable => (lastHitTime + invulnerableTime < Time.time);

    private void Awake()
    {
        spaceShipHealth = spaceShipMaxHealth;
    }

    private void Update()
    {
        if (!isVulnerable)
        {
            if (lastBlinkAnimationTime + blinkDurationTime < Time.time)
            {
                lastBlinkAnimationTime = Time.time;
                blinkActive = !blinkActive;
                meshRenderer.material = blinkActive ? invulnerableMaterial : shipMaterial;
            }
        }
        else if (backFromInvulnerable)
        {
            backFromInvulnerable = false;
            meshRenderer.material = shipMaterial;
        }
    }

    public void Crash()
    {
        if (isVulnerable)
        {
            backFromInvulnerable = true;
            
            spaceShipHealth--;
            
            if (spaceShipHealth <= 0)
            {
                SetDead();
            }

            lastHitTime = Time.time;
        }
    }

    private void SetDead()
    {
        gameOverHandler.EndGame();
        gameObject.SetActive(false);
    }

    public void SetAlive()
    {
        gameObject.SetActive(true);
        spaceShipHealth = spaceShipMaxHealth;
        
        lastHitTime = Time.time + adInvulnerableTime;
    }
}
