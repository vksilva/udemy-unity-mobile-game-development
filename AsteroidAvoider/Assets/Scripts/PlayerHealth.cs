using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int spaceShipHealth;
    [SerializeField] private GameOverHandler gameOverHandler;
    public void Crash()
    {
        spaceShipHealth--;
        if (spaceShipHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        gameOverHandler.EndGame();
        gameObject.SetActive(false);
    }
}
