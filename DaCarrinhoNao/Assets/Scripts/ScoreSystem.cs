using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int scoreMultiplier;

    private float score;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Delta time");
        Debug.Log(Time.deltaTime);
        Debug.Log("score multiplier");
        Debug.Log(scoreMultiplier);
        
        score += Time.deltaTime * scoreMultiplier;
        
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }
}
