using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private MenuDisplay mainMenuDisplay;
    [SerializeField] private MenuDisplay gameDisplay;
    [SerializeField] private GameObject clickToResume;
    [SerializeField] private AudioSource music;
    [SerializeField] private GameObject muteButton;
    [SerializeField] private GameObject unMuteButton;

    private static GameOverDisplay _gameOverDisplay;
    private static MenuDisplay _mainMenuDisplay;
    private static MenuDisplay _gameDisplay;
    private static GameObject _clickToResume;

    private static bool skipMenu;
    private static bool audioMuted;
    public static bool AudioMuted => audioMuted;

    private void Start()
    {
        TogglePaused(true);
        _gameOverDisplay = GetComponentInChildren<GameOverDisplay>();
        _mainMenuDisplay = mainMenuDisplay;
        _gameDisplay = gameDisplay;
        _clickToResume = clickToResume;

        if (skipMenu)
        {
            StartGame();
        }

        audioMuted = audioMuted ? true : PlayerPrefs.GetString("audio") == "off"; // leave audio muted if it is, otherwise check playerprefs
        ToggleAudio(audioMuted);
    }

    public void StartGame()
    {
        _gameDisplay.Show();
        _mainMenuDisplay.Hide();
        TogglePaused(false);
        Stats.Initialize();
    }

    public static void HandleGameOver()
    {
        TogglePaused(true);
        _gameOverDisplay.Show();
        Stats.UpdateAllTimeStats();
    }

    private void OnApplicationQuit()
    {
        AllTimeStats.SaveData();
        PlayerPrefs.Save();
    }

    public void PlayAgain()
    {
        skipMenu = true;
        TogglePaused(false);     
        SceneManager.LoadScene(0);
    }

    public void QuitToMenu()
    {
        skipMenu = false;
        TogglePaused(true);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public static void TogglePaused(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;
    }

    public void ToggleClickToResumeButton(bool active)
    {
        _clickToResume.SetActive(active);
    }

    
    public void ToggleAudio(bool mute)
    {
        audioMuted = mute;
        muteButton.SetActive(mute);
        unMuteButton.SetActive(!mute);

        if (mute) PlayerPrefs.SetString("audio", "off");
        else PlayerPrefs.SetString("audio", "on");
        

        if (mute)
        {
            music.Stop();       
        }

        if (!mute && !music.isPlaying)
        {
            music.Play();
        }
    }

}
