using System;
using System.Collections.Generic;
using Car;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
   // public Vector3 checkPoint;
    public GameObject car;
    public List<CarSO> carsSO = new List<CarSO>();

    public static bool finished;
    public static int livesLeft;
    public static bool isCrahs;
    
    public  int totalDiamonds;

    public delegate void GameFinishDelegate(bool isOpen);
    public event GameFinishDelegate OnGameFinish;

    public delegate void CrashDelegate();
    public event CrashDelegate CrashHandler;

    [Header("StartChecks")]
    public static bool isStart;
    public delegate void StartGame();
    public event StartGame gameStarted;
    
    public Action onClick;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        Application.targetFrameRate = 60;
        
        SaveSystem.Instance.LoadData();
        onClick += Click;
        
    }

    void Update()
    {
        if (isCrahs)
        {
            livesLeft--;
            CrashHandler?.Invoke();
            isCrahs = false;
        }
        if (finished)
        {
            UIManager.Instance.UpdateDiamondText();
            OnGameFinish?.Invoke(true);
        }
        else
        {
            OnGameFinish?.Invoke(false);
        }

        if (isStart)
        {
            gameStarted?.Invoke();
            isStart = false;
        }

       //OnLivesLeft?.Invoke(livesLeft, UIManager.textState);

        if (Input.GetMouseButtonDown(0))
        {
            onClick?.Invoke();
        }
    }

    public void Click()
    {
        car.gameObject.GetComponent<CarController>().SetCarState(CarController.CarState.moving);
    }
    public GameObject GetCar()
    {
       car.gameObject.SetActive(true);
       return car;
    } 

     public void ReturnCar(bool state = true)
     {
        car.SetActive(false);
     }
}