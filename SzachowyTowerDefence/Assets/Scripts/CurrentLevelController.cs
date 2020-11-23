using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CurrentLevelController : MonoBehaviour
{
    [SerializeField] private int startHp;
    [SerializeField] private int startPoints;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text TimeInThisRun;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button backToMainMenuButton;


    public int CurrentHp => currentHp;
    public int StartPoints => startPoints;

    private int currentHp;

    private Stopwatch stopwatch;

    public void Init()
    {
        stopwatch = new Stopwatch();
        stopwatch.Reset();
        stopwatch.Start();
        TimeInThisRun.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        backToMainMenuButton.gameObject.SetActive(false);
        currentHp = startHp;
        hpText.text = currentHp.ToString();
    }

    private void FixedUpdate()
    {
        if(stopwatch != null && stopwatch.IsRunning && TimeInThisRun.gameObject.activeSelf)
        {
            TimeInThisRun.text = stopwatch.Elapsed.Seconds.ToString();
        }
    }

    public void RemoveHp(int hp)
    {
        currentHp -= hp;

        if(currentHp <= 0)
        {
            GameOver();
        }

        hpText.text = currentHp.ToString();
    }

    public void GameOver()
    {
        stopwatch.Stop();
        Time.timeScale = 0f;
        restartButton.gameObject.SetActive(true);
        backToMainMenuButton.gameObject.SetActive(true);
    }

    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void OnBackToMainScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartOne");
    }
}
