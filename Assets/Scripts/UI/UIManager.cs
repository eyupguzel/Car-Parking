using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : GenericSingleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject mainPanel;
    public GameObject levelsPanel;
    public GameObject pausePanel;
    public GameObject finishPanel;
    
    [Header("Buttons")]
    public Button pauseButton;
    public Button NextLevelButton;
    public Button RestartButton;

    public static bool finished;
    public void FinishPanel()
    {
        finishPanel.SetActive(true);
        finished = true;

        if (CarController.isFinish)
        {
            NextLevelButton.GetComponent<Button>().interactable = true;
            RestartButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            NextLevelButton.GetComponent<Button>().interactable = false;
            RestartButton.GetComponent<Button>().interactable = true;
        }
    }

    public void CloseFinishPanel()
    {
        finishPanel.SetActive(false);
    }

    public void PlayButton()
    {
        mainPanel.SetActive(false);
        levelsPanel.SetActive(true);
        
        AudioManager.Instance.SoundPlay(AudioManager.AudioType.Click_1);
    }

    public void ExitBUtton()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        levelsPanel.SetActive(false);
        mainPanel.SetActive(true);
        
        AudioManager.Instance.SoundPlay(AudioManager.AudioType.Click_2);
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void BackMenu()
    {
        GameManager.Instance.ReturnCar();
        LevelManager.Instance.CheckLevelButtons();
        mainPanel.SetActive(true);
        finishPanel.SetActive(false);
        Time.timeScale = 1;
        pauseButton.gameObject.SetActive(false);
        pausePanel.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
