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
      SceneManager.LoadScene(1);
      UIManager.finished = false;
   }
}
