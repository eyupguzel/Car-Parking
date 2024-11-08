using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelManager : GenericSingleton<LevelManager>
{
   [SerializeField] private Button[] levelButtons;
   private int lastLevel;
   private void Start()
   {
      if(lastLevel == 0)
         lastLevel = 1;
      
      CheckLevelButtons();
      LoadingPanel();
   }

   public void CheckLevelButtons()
   {
      for (int i = 0; i < levelButtons.Length; i++)
      {
         if (i <= PlayerPrefs.GetInt("LastLevel"))
         {
            levelButtons[i].interactable = true;
         }
         else
         {
            levelButtons[i].interactable = false;
         }
      }
   }

   private void SetLastLevel()
   {
      PlayerPrefs.SetInt("LastLevel",lastLevel + 1);
      lastLevel = PlayerPrefs.GetInt("LastLevel");
   }

   public void RestartButton()
   {
      AudioManager.Instance.SoundPlay(AudioManager.AudioType.Click_3);

      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      UIManager.Instance.finishPanel.SetActive(false);
   }

   public void NextLevelButton()
   {
      AudioManager.Instance.SoundPlay(AudioManager.AudioType.Click_3);

      UIManager.Instance.finishPanel.SetActive(false);
      SetLastLevel();
      SceneManager.LoadScene(lastLevel);
   }

   public void LoadLevel(int levelNumber)
   {
      AudioManager.Instance.SoundPlay(AudioManager.AudioType.Click_1);

      UIManager.Instance.levelsPanel.SetActive(false);
      if(levelNumber >= lastLevel)
         SetLastLevel();
      
      SceneManager.LoadScene(levelNumber);
      UIManager.Instance.pauseButton.gameObject.SetActive(true);
   }

   private void LoadingPanel()
   {
      if (SceneManager.GetActiveScene().buildIndex == 0)
      {
         StartCoroutine(LoadingCoroutine());
      }
   }

   private IEnumerator LoadingCoroutine()
   {
      yield return  new WaitForSeconds(1.5f);
      SceneManager.LoadScene(1);
   }
}
