using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Stats
{
    private static float _startTime;
    private static int _score;
    private static int _shotsFired;
    private static int _shotsLanded;
    private static int _currentStreak;
    private static int _highestStreak;

    public static event Action<int> ScoreChange;
    public static event Action<int> StreakChange;


    public static void Initialize()
    {
        _startTime = Time.time;
        _shotsFired = 0;
        _shotsLanded = 0;
        _highestStreak = 0;
        _currentStreak = 0;

        ResetScore();
        AllTimeStats.LoadData();
    }

    public static void UpdateAllTimeStats()
    {
        AllTimeStats.UpdateData(new StatsData()
        {
            score = _score,
            shotsFired = _shotsFired,
            shotsLanded = _shotsLanded,
            time = GetTimePlayed(),
            streak = _highestStreak,
        }); ;
    }

    // getters
    public static int GetShotsFired()
    {
        return _shotsFired;
    }
    public static int GetShotsMissed()
    {
        return _shotsFired - _shotsLanded;
    }

    public static TimeSpan GetTimePlayed()
    {
        var seconds = Convert.ToInt32((Time.time - _startTime) % 60);
        return TimeSpan.FromSeconds(seconds);
    }
    public static int GetScore()
    {
        return _score;
    }
    public static double GetAccuracy()
    {
        return Math.Round(((double)_shotsLanded / _shotsFired) * 100f, 1);
    }
    public static int GetHighestStreak()
    {
        return _highestStreak;
    }


    // setters
    private static void ResetScore()
    {
        _score = 0;
        ScoreChange?.Invoke(_score);

    }
    public static void ChangeScore(int amount)
    {
        _score += amount;
        ScoreChange?.Invoke(_score);
    }

    public static void IncrementShotsFired()
    {
        _shotsFired++;
    }

    public static void IncrementShotsLanded()
    {
        _shotsLanded++;
        IncrementStreak();
    }

    public static void IncrementStreak()
    {
        _currentStreak++;
        if (_currentStreak > _highestStreak)
        {
            _highestStreak = _currentStreak;
        }
        StreakChange?.Invoke(_currentStreak);
    }
    public static void StopStreak()
    {
        _currentStreak = 0;
        StreakChange?.Invoke(_currentStreak);
    }
}
