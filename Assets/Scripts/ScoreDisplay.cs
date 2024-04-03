using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI streakText;

    void Start()
    {
        Stats.ScoreChange += UpdateScore;
        Stats.StreakChange += UpdateStreak;
    }

    private void UpdateScore(int currentScore) => scoreText.text = $"Score: {currentScore}";
    private void UpdateStreak(int currentStreak)
    {
        if (currentStreak == 0) streakText.text = "";
        else streakText.text = $"Kill Streak: {currentStreak}";
    }

}
