using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isInfo = false;

    public GameObject pauseUI;
    public GameObject infoUI;

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        infoUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        isInfo = false;
    }

    public void PauseGame()
    {
        if (isPaused == false)
        {
            infoUI.SetActive(false);
            pauseUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }

        else
        {
            ResumeGame();
        }
    }

    public void ShowInfo()
    {
        if (isInfo == false)
        {
            pauseUI.SetActive(false);
            infoUI.SetActive(true);
            Time.timeScale = 0f;
            isInfo = true;
        }

        else
        {
            ResumeGame();
        }
    }
    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("You have quit the game");
        if (UnityEditor.EditorApplication.isPlaying == true)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
