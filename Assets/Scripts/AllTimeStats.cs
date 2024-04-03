using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AllTimeStats
{
    private static int _highScore;
    private static int _bestStreak;
    private static int _totalShotsLanded;
    private static int _totalShotsFired;
    private static int _longestTime;


    private const string SCORE = "highscore";
    private const string STREAK = "beststreak";
    private const string SHOTS_LANDED = "shotslanded";
    private const string SHOTS_FIRED = "shotsfired";
    private const string TIME = "longesttime";

    private static bool _dataLoaded;


    // getters

    public static int HighScore => _highScore;
    public static int TotalShotsFired => _totalShotsFired;
    public static int TotalShotsLanded => _totalShotsLanded;
    public static int BestStreak => _bestStreak;

    public static TimeSpan GetLongestTime()
    {
        return TimeSpan.FromMilliseconds(_longestTime);
    }

    public static double GetAccuracy()
    {
        return Math.Round(((double)_totalShotsLanded / _totalShotsFired) * 100f, 1);
    }

    public static void LoadData()
    {
        if (_dataLoaded) return;

        _highScore = PlayerPrefs.GetInt(SCORE);
        _bestStreak = PlayerPrefs.GetInt(STREAK);
        _totalShotsLanded = PlayerPrefs.GetInt(SHOTS_LANDED);
        _totalShotsFired = PlayerPrefs.GetInt(SHOTS_FIRED);
        _longestTime = PlayerPrefs.GetInt(TIME);

        _dataLoaded = true;
    }

    public static void UpdateData(StatsData gameData) 
    { 
        if (gameData == null)
        {
            Debug.LogWarning("null value provided, failed to update stats");
            return;
        }

        _highScore = Mathf.Max(gameData.score, _highScore);
        _bestStreak = Mathf.Max(gameData.streak, _bestStreak);
        _longestTime = Mathf.Max((int)gameData.time.TotalMilliseconds, _longestTime);

        _totalShotsFired += gameData.shotsFired;
        _totalShotsLanded += gameData.shotsLanded;

    }

    public static void SaveData()
    {
        PlayerPrefs.SetInt(SCORE, _highScore);
        PlayerPrefs.SetInt(STREAK, _bestStreak);
        PlayerPrefs.SetInt(SHOTS_LANDED, _totalShotsLanded);
        PlayerPrefs.SetInt(SHOTS_FIRED, _totalShotsFired);
        PlayerPrefs.SetInt(TIME, _longestTime);
    }

}

public class StatsData
{
    public int score;
    public int streak;
    public TimeSpan time;
    public int shotsLanded;
    public int shotsFired;
    public float accuracy;
}
