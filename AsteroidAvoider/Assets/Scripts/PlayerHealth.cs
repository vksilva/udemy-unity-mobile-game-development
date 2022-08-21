using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int spaceShipHealth;
    public void Crash()
    {
        spaceShipHealth--;
        if (spaceShipHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
