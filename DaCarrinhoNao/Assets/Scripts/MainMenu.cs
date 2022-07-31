using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private TMP_Text energyText;
    [SerializeField] private int maxEnergy;
    [SerializeField] private int energyRechargeDuration;
    [SerializeField] private AndroidNotificationHandler androidNotificationHandler;


    private int energy;

    private const string EnergyKey = "Energy";
    private const string EnergyReadyKey = "EnergyReady";
    private void Start()
    {
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
        
        highScoreText.text = $"High Score:\n {highScore}";

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
        if (energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);
            
            if (energyReadyString == String.Empty) { return; }

            DateTime energyReady = DateTime.Parse(energyReadyString);
            
            if (DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
        }

        energyText.text = $"Play ({energy})";
    }

    public void Play()
    {
        if (energy < 1) return;
        
        //Load game scene
        SceneManager.LoadScene(1);
        ConsumeEnergy();

        if (energy != 0) return;
        
        DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
        PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());
        
        //Set notification to inform player that energy was recharged
#if UNITY_ANDROID
        androidNotificationHandler.ScheduleNotification(energyReady);
#endif
    }

    private void ConsumeEnergy()
    {
        energy--;
        PlayerPrefs.SetInt(EnergyKey, energy);
        
        energyText.text = $"Play ({energy})";
    }
}
