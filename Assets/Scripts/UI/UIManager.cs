using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : GenericSingleton<UIManager>
{
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
}
