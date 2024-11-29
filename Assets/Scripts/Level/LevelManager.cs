using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : GenericSingleton<LevelManager>
{
   [SerializeField] private Button[] levelButtons;
   private int lastLevel;
   [SerializeField] Slider slider;

   private int currentLevel;

   private void Awake()
   {
      DontDestroyOnLoad(this);
      LoadingPanel();
   }
   private void Start()
   {
      lastLevel = SaveSystem.Instance.data.lastLevel;
      CheckLevelButtons();
   }

   public void CheckLevelButtons()
   {
      for (int i = 0; i < levelButtons.Length; i++)
      {
         if (i + 2 <= SaveSystem.Instance.data.lastLevel)
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
   {//        2             1
      if (currentLevel >= lastLevel)
      {
         SaveSystem.Instance.data.lastLevel++;
         lastLevel = SaveSystem.Instance.data.lastLevel;
         currentLevel++;

      }
      else
         currentLevel++;
   }

   public void RestartButton()
   {
      AudioManager.Instance.MenuClickSound3();
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void NextLevelButton()
   {
      AudioManager.Instance.MenuClickSound3();
      SetLastLevel(); 
      SceneManager.LoadScene(currentLevel);
   }

   public void LoadLevel(int levelNumber)
   {
      AudioManager.Instance.MenuClickSound1();

      UIManager.Instance.levelsPanel.SetActive(false);
      if (levelNumber >= lastLevel)
      {
         lastLevel = levelNumber;
      }
      else
      {
         currentLevel = levelNumber;
      }
      
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
      slider.value += .4f;
      yield return  new WaitForSeconds(1.5f);
      SceneManager.LoadScene(1);
   }
}
