using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : GenericSingleton<LevelManager>
{
   private void Awake()
   {
      DontDestroyOnLoad(gameObject);
   }

   public void RestartButton()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      UIManager.Instance.finishPanel.SetActive(false);
   }

   public void NextLevelButton()
   {
      UIManager.Instance.finishPanel.SetActive(false);
      SceneManager.LoadScene(1);
   }

   public void LoadLevel1()
   {
      UIManager.Instance.levelsPanel.SetActive(false);
      SceneManager.LoadScene(0);
   }
}
