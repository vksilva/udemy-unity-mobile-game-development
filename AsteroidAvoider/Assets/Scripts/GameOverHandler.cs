using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;

    private float score;
    private string score_text;
    public void EndGame()
    {
        score = scoreSystem.GetScore();
        score_text = Mathf.FloorToInt(score).ToString();
        
        asteroidSpawner.enabled = false;
        scoreSystem.enabled = false;
        
        this.gameObject.SetActive(true);
        gameOverText.text = $"Your score is {score_text}";

    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void Continue()
    {
        
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
