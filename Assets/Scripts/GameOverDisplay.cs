using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    public GameObject gameOverScreen;

    public TextMeshProUGUI timePlayedText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI shotsFiredText;
    public TextMeshProUGUI shotsMissedText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI streakText;


    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    public void Show()
    {
        gameOverScreen.SetActive(true);
        timePlayedText.text = Stats.GetTimePlayed().ToString();
        scoreText.text = Stats.GetScore().ToString();
        shotsFiredText.text = Stats.GetShotsFired().ToString();
        shotsMissedText.text = Stats.GetShotsMissed().ToString();
        accuracyText.text = $"{Stats.GetAccuracy()} %";
        streakText.text = Stats.GetHighestStreak().ToString();
        
    }
}
