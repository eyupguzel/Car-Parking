using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public List<GameObject> cars =  new List<GameObject>();
    public static int carCount;
    public bool click;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var car in cars)
            {
                if (car.activeInHierarchy)
                {
                    car.gameObject.GetComponent<CarController>().SetCarState(CarController.CarState.moving);
                    //break;
                }
            }
        }
    }

    public List<GameObject> GetCarPool(int carCount)
    {
        List<GameObject> carsToUse = new List<GameObject>();

        int index = 0;
        foreach (GameObject car in cars)
        {
            if (!car.activeInHierarchy)
            {
                carsToUse.Add(car);
                index++;

                if (index >= carCount) 
                    break;
            }
            
        }
        return carsToUse;
    }
    
    public void ReturnCar(List<GameObject> cars)
    {
        foreach (GameObject car in cars)
        {
            car.SetActive(false);
        }
    }

    public void CheckCarCount()
    {
        if(carCount <= 0)
            UIManager.Instance.FinishPanel();
    }

    public void ClickFalse()
    {
        click = false;
    }
}