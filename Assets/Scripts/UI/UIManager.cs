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
    [SerializeField] GameObject finishPanel;
    [SerializeField] GameObject infoPanel;
    
    [Header("Buttons")]
    public Button pauseButton;
    public Button NextLevelButton;
    public Button RestartButton;

    [Header("Stats")] 
    public TextMeshProUGUI diamondText;
    public Image health1;
    public Image health2;
    public Image health3;
    public Sprite activeHealth;
    public Sprite pasiveHealth;
    void Start()
    {
        GameManager.Instance.gameStarted += SetActiveInfoPanel;
        GameManager.Instance.OnGameFinish += FinishPanel;
        GameManager.Instance.CrashHandler += LivesLeftUpdate;
        GameManager.Instance.gameStarted += LivesLeftUpdate;
        
        CarController.collectDiamond += UpdateDiamondText;
    }
    public void LivesLeftUpdate()
    {
        if (GameManager.livesLeft == 3)
        {
            health1.sprite = activeHealth;
            health2.sprite = activeHealth;
            health3.sprite = activeHealth;
        }
        else if (GameManager.livesLeft == 2)
        {
            health1.sprite = activeHealth;
            health2.sprite = activeHealth;
            health3.sprite = pasiveHealth;
        }
        else
        {
            health1.sprite = activeHealth;
            health2.sprite = pasiveHealth;
            health3.sprite = pasiveHealth;
        }

    }
    private void FinishPanel(bool isOpen)
    {

        if (isOpen)
        {
            finishPanel.SetActive(true);
            infoPanel.SetActive(false);

            if (GameManager.finished)
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
        else
        {
            finishPanel.gameObject.SetActive(false);
        }
    }
    public void PlayButton()
    {
        mainPanel.SetActive(false);
        levelsPanel.SetActive(true);
        
        AudioManager.Instance.MenuClickSound1();
    }

    public void ExitBUtton()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        levelsPanel.SetActive(false);
        mainPanel.SetActive(true);
        
        AudioManager.Instance.MenuClickSound2();
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void UpdateDiamondText()
    {
        diamondText.text = $"{SaveSystem.Instance.data.totalDiamonds}";
    }

    public void GoToGarage()
    {
        mainPanel.SetActive(false);
        SceneManager.LoadScene("Garage");
    }

    public void BackMenu()
    {
        GameManager.Instance.ReturnCar();
        GameManager.finished = false;
        LevelManager.Instance.CheckLevelButtons();
        mainPanel.SetActive(true);
        finishPanel.SetActive(false);
        Time.timeScale = 1;
        pauseButton.gameObject.SetActive(false);
        pausePanel.SetActive(false);
        SceneManager.LoadScene(1);
    }

    public void SetActiveInfoPanel()
    {
        infoPanel.SetActive(true);
    }
}
