using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AllTimeStatsDisplay : MonoBehaviour
{
    public GameObject statsDisplay;

    public TextMeshProUGUI timePlayedText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI shotsFiredText;
    public TextMeshProUGUI shotsMissedText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI streakText;


    private void Awake()
    {
        statsDisplay.SetActive(false);
    }

    public void Show()
    {

        statsDisplay.SetActive(true);
        timePlayedText.text = AllTimeStats.GetLongestTime().ToString();
        scoreText.text = AllTimeStats.HighScore.ToString();
        shotsFiredText.text = AllTimeStats.TotalShotsFired.ToString();
        shotsMissedText.text = AllTimeStats.TotalShotsLanded.ToString();
        accuracyText.text = $"{AllTimeStats.GetAccuracy()} %";
        streakText.text = AllTimeStats.BestStreak.ToString();

    }

    public void Hide()
    {
        statsDisplay.SetActive(false);
    }
}
