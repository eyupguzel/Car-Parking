using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : GenericSingleton<UIManager>
{
    [SerializeField] private GameObject mainPanel;
    public GameObject levelsPanel;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField] private TextMeshProUGUI carCountText;
    public GameObject finishPanel;
    public static bool finished;
    public void CarCountText(int carCount)
    {
        carCountText.text = $"{carCount}";
    }

    public void FinishPanel()
    {
        finishPanel.SetActive(true);
        finished = true;
    }

    public void CloseFinishPanel()
    {
        finishPanel.SetActive(false);

    }

    public void PlayButton()
    {
        mainPanel.SetActive(false);
        levelsPanel.SetActive(true);
    }

    public void ExitBUtton()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        levelsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
